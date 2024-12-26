using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCCoreCF.Data;
using MVCCoreCF.Models;

namespace MVCCoreCF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ApplicationContext context;

        public HomeController(ILogger<HomeController> logger1, ApplicationContext context1)
        {
            logger = logger1;
            context = context1;
        }

        public IActionResult FirstPage()
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
