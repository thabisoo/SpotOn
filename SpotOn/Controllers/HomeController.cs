using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotOn.Models;

namespace SpotOn.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            var loggedInUser = HttpContext.User.Claims;

            if (loggedInUser.Any())
                return RedirectToAction("Index", "Post");

            return RedirectToAction("LogIn", "Auth");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
