using ClickTixWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClickTixWeb.Controllers
{
    public class DetalleController : Controller
    {

        private readonly ClicktixContext _context;
        private readonly ILogger<DetalleController> _logger;


        // GET: DetalleController
        public ActionResult Index()
        {

            return View();
        }
        public DetalleController(ILogger<DetalleController> logger, ClicktixContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult CambioFecha(string fecha, int idPelicula)
        {
            DateOnly fechaSeleccionada = DateOnly.FromDateTime(DateTime.Parse(fecha).Date);

            var turnos = ObtenerTurnosPorFechaYIdPelicula(fechaSeleccionada, idPelicula);



            return PartialView("_TurnosPartial", turnos);
        }


        public IActionResult SeleccionarButaca(int funcionId)
        {
            var funcion = _context.Funcions.Find(funcionId);

            if (funcion == null)
            {
                return RedirectToAction("Index");
            }

            var (filas, columnas) = ObtenerFilasYColumnasPorFuncion(funcionId);

            List<Asiento> asientos = ObtenerAsientosPorFuncion(funcionId);

            var viewModel = new SeleccionButacasViewModel
            {
                Funcion = funcion,
                Filas = filas,
                Columnas = columnas,
                Asientos = asientos
            };

            return View("~/Views/SeleccionButacas/Index.cshtml", viewModel);
        }

        private List<Turno> ObtenerTurnosPorFechaYIdPelicula(DateOnly fecha, int idPelicula)
        {

            var turnos = _context.Funcions
                .Where(f => f.IdPelicula == idPelicula && f.Fecha == fecha)
                .Select(f => new Turno
                {
                })
                .ToList();

            return turnos;
        }

        public List<Asiento> ObtenerAsientosPorFuncion(int idFuncion)
        {
            List<Asiento> listaAsientos = _context.Asientos
                .Where(a => a.IdFuncion == idFuncion)
                .ToList();

            return listaAsientos;
        }

        public (int Filas, int Columnas) ObtenerFilasYColumnasPorFuncion(int idFuncion)
        {
            var resultado = (from s in _context.Salas
                             join f in _context.Funcions on s.Id equals f.IdSala
                             join a in _context.Asientos on f.Id equals a.IdFuncion
                             where f.Id == idFuncion
                             select new { s.Filas, s.Columnas })
                             .FirstOrDefault();

            return (resultado?.Filas ?? 0, resultado?.Columnas ?? 0);
        }
        // GET: DetalleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DetalleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DetalleController/Create
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

        // GET: DetalleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetalleController/Edit/5
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

        // GET: DetalleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetalleController/Delete/5
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
