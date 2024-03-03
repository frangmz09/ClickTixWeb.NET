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


            funcionStrings.Idioma = ObtenerNombreIdioma(funcionStrings.Idioma);

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




        public IActionResult Detalle(int peliculaId, int? SucursalId)
        {
            var pelicula = _context.Peliculas.Find(peliculaId);

            if (pelicula == null)
            {
                return RedirectToAction("Index");
            }

            var fechaActual = DateOnly.FromDateTime(DateTime.Now.Date);

            var proximasFuncionesQuery = _context.Funcions
                .Where(f => f.IdPelicula == peliculaId
                            && f.Fecha >= fechaActual
                            && f.Fecha <= fechaActual.AddDays(7)
                            && (!SucursalId.HasValue || _context.Salas.Any(s => s.Id == f.IdSala && s.IdSucursal == SucursalId)))
                .OrderBy(f => f.Fecha)
                .ToList();

            var proximasFuncionesList = proximasFuncionesQuery
                .ToList();

            List<FuncionStrings> ProximasFuncionesStrings = new List<FuncionStrings>();

            var fechasUnicas = _context.Funcions
                .Where(f => f.IdPelicula == peliculaId && f.Fecha >= fechaActual && f.Fecha <= fechaActual.AddDays(7))
                .OrderBy(f => f.Fecha)
                .Where(f => !SucursalId.HasValue || _context.Salas.Any(s => s.Id == f.IdSala && s.IdSucursal == SucursalId))
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
                CategoriaString = ObtenerCategoriaPorPeliculaId(peliculaId),
                ClasificacionString = ObtenerClasificacionPorPeliculaId(peliculaId),

            };

            var sucursalesConFunciones = _context.Sucursals
                .Where(sucursal => sucursal.Salas
                    .Any(sala => sala.Funcions
                        .Any(funcion => funcion.IdPelicula == peliculaId)))
                .ToList();
            viewModel.Sucursales = sucursalesConFunciones;

            if (SucursalId.HasValue)
            {
                viewModel.sucursalId = SucursalId;
                viewModel.sucursalNombre = _context.Sucursals
        .Where(s => s.Id == SucursalId)
        .Select(s => s.Nombre)
        .FirstOrDefault();
            }

            return View("~/Views/Detalle/Index.cshtml", viewModel);
        }
        public string ObtenerCategoriaPorPeliculaId(int peliculaId)
        {
            var categoria = _context.Peliculas
               .Where(p => p.Id == peliculaId)
               .Select(p => p.IdCategoriaNavigation.Nombre)
               .FirstOrDefault();

            return categoria;
        }
        public string ObtenerClasificacionPorPeliculaId(int peliculaId)
        {
            var clasificacion = _context.Peliculas
                .Where(p => p.Id == peliculaId)
                .Select(p => p.IdClasificacionNavigation.Clasificacion1)
                .FirstOrDefault();

            return clasificacion;
        }
        public IActionResult Index(int? sucursalId)
        {
            var fechaActual = DateOnly.FromDateTime(DateTime.Now.Date);

            var query = _context.Peliculas
                .Where(p => _context.Funcions
                    .Any(f => f.IdPelicula == p.Id && f.Fecha >= fechaActual));
            var peliculasConFunciones = query.ToList();


            if (sucursalId.HasValue)
            {
               
                peliculasConFunciones = peliculasConFunciones
                .Join(_context.Funcions,
                    pelicula => pelicula.Id,
                    funcion => funcion.IdPelicula,
                    (pelicula, funcion) => new { Pelicula = pelicula, Funcion = funcion })
                .Join(_context.Salas,
                    peliculaFuncion => peliculaFuncion.Funcion.IdSala,
                    sala => sala.Id,
                    (peliculaFuncion, sala) => new { peliculaFuncion.Pelicula, peliculaFuncion.Funcion, Sala = sala })
                .Join(_context.Sucursals,
                    sala => sala.Sala.IdSucursal,
                    sucursal => sucursal.Id,
                    (sala, sucursal) => new { sala.Pelicula, sala.Funcion, Sala = sala.Sala, Sucursal = sucursal })
                .Where(s => s.Sucursal.Id == sucursalId)
                .Select(s => s.Pelicula)
                .Distinct()
                .ToList();
            }

            var sucursales = _context.Sucursals.ToList();

            var model = new HomeViewModel
            {
                Peliculas = peliculasConFunciones,
                Sucursales = sucursales,
                SucursalId = sucursalId
            };

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