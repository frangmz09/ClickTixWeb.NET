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
        public int ObtenerFilaPorId(int asientoId)
        {
            var fila = _context.Asientos
                .Where(asiento => asiento.Id == asientoId)
                .Select(asiento => asiento.Fila)
                .FirstOrDefault();

            return (int)fila;
        }
        public int ObtenerColumnaPorId(int asientoId)
        {
            var fila = _context.Asientos
                .Where(asiento => asiento.Id == asientoId)
                .Select(asiento => asiento.Columna)
                .FirstOrDefault();

            return (int)fila;
        }

        public IActionResult ConfirmarButacas(int idFuncion, string selectedSeats)
        {


            FuncionStrings funcionStrings = new FuncionStrings();

            var funcionEncontrada = _context.Funcions.Find(idFuncion);



            if (funcionEncontrada == null)
            {
                return RedirectToAction("Index");
            }

            FuncionStrings fs =  ObtenerFuncionStrings(idFuncion);

            string portadaDePelicula = (from pelicula in _context.Peliculas
                                     join funcion in _context.Funcions on pelicula.Id equals funcion.IdPelicula
                                     where funcion.Id == idFuncion
                                     select pelicula.Portada)
                        .FirstOrDefault();


            List<int> idsAsientos = new List<int>();
            List<int> filasAsientos = new List<int>();
            List<int> columnasAsientos = new List<int>();


            int[] seatIds = JsonConvert.DeserializeObject<int[]>(selectedSeats);

            foreach (int asientoId in seatIds) {
                idsAsientos.Add(asientoId);
                filasAsientos.Add(ObtenerFilaPorId(asientoId));
                columnasAsientos.Add(ObtenerColumnaPorId(asientoId));

            };

            var viewModel = new confirmarButacasViewModel
            {
                portadaDePelicula = portadaDePelicula,
                Funcion = funcionEncontrada,
                AsientosId = idsAsientos,
                AsientosFilas = filasAsientos,
                AsientosColumnas = columnasAsientos,
                FuncionStrings = fs,
                Asientos = _context.Asientos.Where(asiento => idsAsientos.Contains(asiento.Id)).ToList(),
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
                                  join turno in _context.Turnos on f.TurnoId equals turno.Id
                                  join sucursal in _context.Sucursals on sala.IdSucursal equals sucursal.Id
                                  where f.Id == funcionIdABuscar
                                  select new FuncionStrings
                                  {
                                      Dimension = dimension.Dimension1,
                                      Pelicula = pelicula.Titulo,
                                      Sala = sala.NroSala.ToString(),
                                      Turno = turno.Hora.ToString(),
                                      Idioma = f.IdiomaFuncion,
                                      Sucursal = sucursal.Nombre,
                                      CuitSucursal = sucursal.Cuit.ToString(),
                                      precioFuncion = (double)dimension.Precio,
                                      Fecha = f.Fecha.ToString(),
                                      Id = f.Id

                                  }).FirstOrDefault();



            return funcionStrings;
        }
        public string ObtenerNombreIdioma(string idioma)
        {

            int idiomaId = int.Parse(idioma);

            var nombreIdioma = _context.Idiomas
                .Where(idioma => idioma.Id == idiomaId)
                .Select(idioma => idioma.Idioma1)
                .FirstOrDefault() ?? "Sin idioma";

            return nombreIdioma;
        }
    }
}
