using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP.NET.Models;

namespace ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult login()
        {
            
            return View();
        }

        public IActionResult book1()
        {
            return View();
        }

        public IActionResult register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult register(account a)
        {
            if (ModelState.IsValid)
                return View();
            else
                return View();


        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
