using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoggingAndMonitoring.Models;
using PerformanceCounterHelper;
using LoggingAndMonitoring.Infrastructure;

namespace LoggingAndMonitoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var counterHelper = PerformanceHelper.CreateCounterHelper<Counters>("");
            counterHelper.Increment(Counters.GoToIndex);

            _logger.LogError("Request header are empty");

            _logger.LogInformation("Method index finished");

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
