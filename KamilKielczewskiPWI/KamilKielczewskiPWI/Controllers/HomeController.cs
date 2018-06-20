using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KamilKielczewskiPWI.Models;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace KamilKielczewskiPWI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IStringLocalizer<HomeController> localizer, ILogger<HomeController> logger)
        {
            _localizer = localizer;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation(_localizer["Hello"]);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
