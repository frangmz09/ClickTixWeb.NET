using ClickTixWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClickTixWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly ClicktixContext _context;


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ClicktixContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var peliculas = _context.Peliculas.ToList();
            return View(peliculas);
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