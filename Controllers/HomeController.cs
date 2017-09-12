using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BirdieBook.Models;
using Microsoft.Extensions.Logging;

namespace BirdieBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggerFactory _loggerFactory;

        public HomeController(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            var logger = _loggerFactory.CreateLogger("LoggerCategory");

            logger.LogInformation("HomeController created");

        }
        public IActionResult Index()
        {
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
