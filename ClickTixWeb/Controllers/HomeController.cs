﻿using ClickTixWeb.Models;
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
                                      Fecha =  f.Fecha.ToString(),
                                      Id = f.Id
                                  }).FirstOrDefault();

            return funcionStrings;
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

            List<FuncionStrings> ProximasFuncionesStrings = new List<FuncionStrings>();

            var fechasUnicas = _context.Funcions
            .Where(f => f.IdPelicula == peliculaId && f.Fecha > fechaActual && f.Fecha <= fechaActual.AddDays(7))
            .OrderBy(f => f.Fecha)
            .Select(f => f.Fecha)
            .Where(fecha => fecha.HasValue)
            .Select(fecha => fecha!.Value)
            .Distinct()
            .ToList();

            foreach (var funcion in proximasFunciones)
            {
                ProximasFuncionesStrings.Add(ObtenerFuncionStrings(funcion.Id));

            }

            var proximasFuncionesStrings = _context.Funcions
            .Where(f => f.IdPelicula == peliculaId && f.Fecha > fechaActual && f.Fecha <= fechaActual.AddDays(7))
            .OrderBy(f => f.Fecha)
            .ToList();

            var viewModel = new DetalleViewModel
            {
                Pelicula = pelicula,
                ProximasFunciones = proximasFunciones,
                FechasUnicas = fechasUnicas,
                ProximasFuncionesStrings = ProximasFuncionesStrings,
            };



            return View("~/Views/Detalle/Index.cshtml", viewModel);
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