using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPHSS.MVCApp.FE.Models;
using System.Diagnostics;

namespace SPHSS.MVCApp.FE.Controllers
{
    [Authorize(Roles = "1,2")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
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

        public async Task<IActionResult> Forbidden()
        {
            return View();
        }
    }
}
