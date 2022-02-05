using HorseVito.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorseVito.Controllers
{
    public class AdvInfoController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;

        public AdvInfoController(DataBaseContext dataBaseContext)
        {

            _dataBaseContext = dataBaseContext;
        }

        public IActionResult AdvInfo(int AdvId)
        {
            ViewBag.People = _dataBaseContext.People;

            ViewBag.Adv = _dataBaseContext.Advertisements.Include(i => i.Owner);
            ViewBag.AdvId = AdvId;
            return View(); 
        }
    }
}
