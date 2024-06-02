using EmlakPortal.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EmlakPortal.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration = null)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult AllListings()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();
        }

        public IActionResult Categories()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();
        }
        
        public IActionResult Lands()
        {
            ViewBag.ApiBaseUrl = _configuration["ApiBaseUrl"];
            return View();
        }      
        
        public IActionResult Properties()
        {
            ViewBag.ApiBaseUrl = _configuration["ApiBaseUrl"];
            return View();
        }

        public IActionResult AddLand()
        {
            ViewBag.ApiBaseUrl = _configuration["ApiBaseUrl"];
            return View();
        }

        public IActionResult AddProperty()
        {
            ViewBag.ApiBaseUrl = _configuration["ApiBaseUrl"];
            return View();
        }

        public IActionResult EditLand(int id)
        {
            ViewBag.ApiBaseUrl = _configuration["ApiBaseUrl"];
            ViewBag.LandId = id;
            return View();
        }

        public IActionResult EditProperty(int id)
        {
            ViewBag.ApiBaseUrl = _configuration["ApiBaseUrl"];
            ViewBag.PropertyId = id;
            return View();
        }

        public IActionResult DeleteLand(int id)
        {
            ViewBag.ApiBaseUrl = _configuration["ApiBaseUrl"];
            ViewBag.LandId = id;
            return View();
        }

        public IActionResult DeleteProperty(int id)
        {
            ViewBag.ApiBaseUrl = _configuration["ApiBaseUrl"];
            ViewBag.PropertyId = id;
            return View();
        }

        public IActionResult LandDetails(int id)
        {
            ViewBag.ApiBaseUrl = _configuration["ApiBaseUrl"];
            ViewBag.ItemId = id;
            return View();
        }

        public IActionResult PropertyDetails(int id)
        {
            ViewBag.ApiBaseUrl = _configuration["ApiBaseUrl"];
            ViewBag.ItemId = id;
            return View();
        }

        public IActionResult MyListings()
        {
            ViewBag.ApiBaseUrl = _configuration["ApiBaseUrl"];
            return View(); 
        }

        public IActionResult Profile()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();

        }

        [Route("Login")]
        public IActionResult Login()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();
        }

        [Route("Signup")]
        public IActionResult SignUp()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
