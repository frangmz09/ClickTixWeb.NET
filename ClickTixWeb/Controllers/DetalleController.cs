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

        public IActionResult DetalleUpdate(int peliculaId, int? sucursalId)
        {
            var pelicula = _context.Peliculas.Find(peliculaId);

            if (pelicula == null)
            {
                return RedirectToAction("Index");
            }

            var fechaActual = DateOnly.FromDateTime(DateTime.Now.Date);

            var proximasFuncionesQuery = _context.Funcions
                .Where(f => f.IdPelicula == peliculaId
                            && f.Fecha > fechaActual
                            && f.Fecha <= fechaActual.AddDays(7)
                            && (!sucursalId.HasValue || _context.Salas.Any(s => s.Id == f.IdSala && s.IdSucursal == sucursalId)))
                .OrderBy(f => f.Fecha)
                .ToList();

            var proximasFuncionesList = proximasFuncionesQuery
                .ToList();

            List<FuncionStrings> ProximasFuncionesStrings = new List<FuncionStrings>();

            var fechasUnicas = _context.Funcions
                .Where(f => f.IdPelicula == peliculaId && f.Fecha > fechaActual && f.Fecha <= fechaActual.AddDays(7))
                .OrderBy(f => f.Fecha)
                .Where(f => !sucursalId.HasValue || _context.Salas.Any(s => s.Id == f.IdSala && s.IdSucursal == sucursalId))
                .Select(f => f.Fecha)
                .Where(fecha => fecha.HasValue)
                .Select(fecha => fecha!.Value)
                .Distinct()
                .ToList();

            foreach (var funcion in proximasFuncionesList)
            {
                ProximasFuncionesStrings.Add(ObtenerFuncionStrings(funcion.Id));
            }

            var viewModel = new DetalleViewModel
            {
                Pelicula = pelicula,
                ProximasFunciones = proximasFuncionesList,
                FechasUnicas = fechasUnicas,
                ProximasFuncionesStrings = ProximasFuncionesStrings,
            };

            var sucursalesConFunciones = _context.Sucursals
                .Where(sucursal => sucursal.Salas
                    .Any(sala => sala.Funcions
                        .Any(funcion => funcion.IdPelicula == peliculaId)))
                .ToList();
            viewModel.Sucursales = sucursalesConFunciones;

            if (sucursalId.HasValue)
            {
                viewModel.sucursalId = sucursalId;
                viewModel.sucursalNombre = _context.Sucursals
        .Where(s => s.Id == sucursalId)
        .Select(s => s.Nombre)
        .FirstOrDefault();
            }

            return View("Index", viewModel);
        }

        public IActionResult SeleccionarButaca(int funcionId)
        {
            var funcion = _context.Funcions.Find(funcionId);
            var funcionStrings = ObtenerFuncionStrings(funcionId);

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
                Asientos = asientos,
                FuncionStrings = funcionStrings,
            };

            return View("~/Views/SeleccionButacas/Index.cshtml", viewModel);
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
                                      Fecha = f.Fecha.ToString(),
                                      Id = f.Id
                                  }).FirstOrDefault();

            return funcionStrings;
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
