using HorseVito.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HorseVito.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public LoginController(DataBaseContext dataBaseContext, IWebHostEnvironment hostingEnvironment)
        {

            _dataBaseContext = dataBaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                HumanModel human = await _dataBaseContext.People
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
                if (human != null)
                {
                    await Authenticate(human); // аутентификация

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        private async Task Authenticate(HumanModel person)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Username),

                new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public IActionResult Reg()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reg(RegModel model)
        {
            
            

            if (ModelState.IsValid)
            {

                

                HumanModel person = await _dataBaseContext.People.FirstOrDefaultAsync(u => u.Username == model.Username);
                if (person == null)
                {
                    using (var stream = new FileStream(Path.Combine(_hostingEnvironment.WebRootPath, @"themes\images\", model.Avatar.FileName), FileMode.Create))
                    {
                        model.Avatar.CopyTo(stream);
                    }


                    // добавляем пользователя в бд
                    person = new HumanModel { Username = model.Username, Password = model.Password, Avatar = "../themes/images/" +
                                                model.Avatar.FileName, PhoneNumber = model.PhoneNumber };

                    Role userRole = await _dataBaseContext.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                        person.Role = userRole;

                    _dataBaseContext.People.Add(person);
                    await _dataBaseContext.SaveChangesAsync();

                    await Authenticate(person); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким именем уже зарегистрирован!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Не все поля заполнены или данные некорректны!");
            }
            return View(model);
        }
    }
}
    