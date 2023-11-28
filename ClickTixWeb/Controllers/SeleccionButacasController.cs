using ClickTixWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SQLitePCL;
using System.Diagnostics;

namespace ClickTixWeb.Controllers
{
    public class SeleccionButacasController : Controller
    {

        private readonly ClicktixContext _context;
        private readonly ILogger<DetalleController> _logger;


        public SeleccionButacasController(ILogger<DetalleController> logger, ClicktixContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult ConfirmarButacas(int idFuncion, string selectedSeats)
        {

            var funcionEncontrada = _context.Funcions.Find(idFuncion);

            var nombrePelicula = from funcion in _context.Funcions
                                 join pelicula in _context.Peliculas on funcion.IdPelicula equals pelicula.Id
                                 where funcion.IdPelicula == funcionEncontrada.IdPelicula
                                 select new
                                 {
                                     TituloPelicula = pelicula.Titulo
                                 };

            string tituloFinal = "";
            foreach (var item in nombrePelicula)
            {
                tituloFinal = item.TituloPelicula;
            }



            int[] seatIds = JsonConvert.DeserializeObject<int[]>(selectedSeats);
            if (funcionEncontrada == null)
            {
                return RedirectToAction("Index");
            }


            var viewModel = new confirmarButacasViewModel
            {
                Funcion = funcionEncontrada,
                Asientos = seatIds,
                TituloPelicula = tituloFinal,
            };
            return View("~/Views/Compra/Index.cshtml", viewModel);
        }






        // GET: SeleccionButacasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SeleccionButacasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SeleccionButacasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeleccionButacasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SeleccionButacasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SeleccionButacasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SeleccionButacasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SeleccionButacasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
