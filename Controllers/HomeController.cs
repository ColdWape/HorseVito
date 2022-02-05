using HorseVito.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HorseVito.Controllers
{
    public class HomeController : Controller
    {

        private readonly DataBaseContext _dataBaseContext;


        public HomeController(DataBaseContext dataBaseContext)
        {

            _dataBaseContext = dataBaseContext;
        }

        public IActionResult Index()
        {
            ViewBag.People = _dataBaseContext.People;
            ViewBag.Adv = _dataBaseContext.Advertisements.OrderByDescending(u => u.Id); ;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
