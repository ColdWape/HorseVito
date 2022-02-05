using HorseVito.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HorseVito.Controllers
{
    public class CreateAdvertismentController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateAdvertismentController(DataBaseContext dataBaseContext, IWebHostEnvironment hostingEnvironment)
        {

            _dataBaseContext = dataBaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Adv()
        {
            ViewBag.People = _dataBaseContext.People;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adv(AdvertisementModel model,IFormFile HorsePhoto, string SelectedGender, string DescriptionTxt)
        {

            if (ModelState.IsValid && HorsePhoto != null && SelectedGender != null && DescriptionTxt != null)
            {

                HumanModel human = await _dataBaseContext.People.FirstOrDefaultAsync(i => i.Username == User.Identity.Name);

                using (var stream = new FileStream(Path.Combine(_hostingEnvironment.WebRootPath, @"themes\images\Horses\", HorsePhoto.FileName), FileMode.Create))
                {
                    HorsePhoto.CopyTo(stream);
                }

                AdvertisementModel advertisement = new AdvertisementModel
                {
                    Name = model.Name,
                    Breed = model.Breed,
                    Gender = SelectedGender,
                    Height = model.Height,
                    Age = model.Age,
                    Price = model.Price,
                    Photo = "../themes/images/Horses/" + HorsePhoto.FileName,
                    Description = DescriptionTxt,
                    Owner = human,
                    Status = "Модерация"

                };
                _dataBaseContext.Advertisements.Add(advertisement);
                    await _dataBaseContext.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                ModelState.AddModelError("", "Не все поля заполнены или данные некорректны!");
            }
            return View(model);
        }
    }
}