using LifeiOS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using LifeiOS.Data;
using Microsoft.AspNetCore.Diagnostics;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        //[AllowAnonymous]
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        ////public IActionResult Error()
        ////{
        ////    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        ////}
        public IActionResult Error()
        {
            return View();
        }

        public IActionResult ErrorStatus(int code)
        {
            ViewBag.StatusCode = code;

            return View("Error");
        }
    }
}
