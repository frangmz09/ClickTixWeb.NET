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


            FuncionStrings funcionStrings = new FuncionStrings();

            var funcionEncontrada = _context.Funcions.Find(idFuncion);



            FuncionStrings fs =  ObtenerFuncionStrings(idFuncion);







            int[] seatIds = JsonConvert.DeserializeObject<int[]>(selectedSeats);
            if (funcionEncontrada == null)
            {
                return RedirectToAction("Index");
            }


            var viewModel = new confirmarButacasViewModel
            {
                Funcion = funcionEncontrada,
                Asientos = seatIds,
                FuncionStrings = fs,
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

        public FuncionStrings ObtenerFuncionStrings(int funcionIdABuscar)
        {
            var funcionStrings = (from f in _context.Funcions
                                  join pelicula in _context.Peliculas on f.IdPelicula equals pelicula.Id
                                  join dimension in _context.Dimensions on f.IdDimension equals dimension.Id
                                  join sala in _context.Salas on f.IdSala equals sala.Id
                                  join idiomaGroup in _context.Idiomas on f.IdiomaFuncion equals idiomaGroup.Idioma1 into idiomaGroup
                                  from idioma in idiomaGroup.DefaultIfEmpty()  // Left join
                                  join turno in _context.Turnos on f.TurnoId equals turno.Id
                                  join sucursal in _context.Sucursals on sala.IdSucursal equals sucursal.Id
                                  where f.Id == funcionIdABuscar
                                  select new FuncionStrings
                                  {
                                      Dimension = dimension.Dimension1,
                                      Pelicula = pelicula.Titulo,
                                      Sala = sala.NroSala.ToString(),
                                      Idioma = idioma != null ? idioma.Idioma1 : "Sin idioma",  // Manejar la posibilidad de un idioma nulo
                                      Turno = turno.Hora.ToString(),
                                      Sucursal = sucursal.Nombre,
                                      CuitSucursal = sucursal.Cuit.ToString(),
                                  }).FirstOrDefault();

            return funcionStrings;
        }
    }
}
