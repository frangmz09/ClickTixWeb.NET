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

        [HttpPost]
        public IActionResult Detalle(int peliculaId)
        {
            var pelicula = _context.Peliculas.Find(peliculaId);

            if (pelicula == null)
            {
                return RedirectToAction("Index");
            }

            var fechaActual = DateOnly.FromDateTime(DateTime.Now.Date);

            var proximasFunciones = _context.Funcions
            .Where(f => f.IdPelicula == peliculaId && f.Fecha > fechaActual && f.Fecha <= fechaActual.AddDays(7))
            .OrderBy(f => f.Fecha)
            .ToList();

            var model = new DetalleViewModel(); 
            model.Pelicula = pelicula;
            model.ProximasFunciones = proximasFunciones;
            return View("~/Views/Detalle/Index.cshtml", model);
        }
        public IActionResult Index()
        {
            var fechaActual = DateOnly.FromDateTime(DateTime.Now.Date);

            var peliculasConFunciones = _context.Peliculas
                .Where(p => _context.Funcions.Any(f => f.IdPelicula == p.Id && f.Fecha > fechaActual))
                .ToList();

            var sucursales = _context.Sucursals.ToList();  // Obtener todas las sucursales

            var model = new Tuple<List<Pelicula>, List<Sucursal>>(peliculasConFunciones, sucursales);

            return View(model);

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