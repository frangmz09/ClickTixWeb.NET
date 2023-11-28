﻿using ClickTixWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickTixWeb.Controllers
{
    public class DetalleController : Controller
    {

        private readonly ClicktixContext _context;

        // GET: DetalleController
        public ActionResult Index()
        {

            return View();
        }
        public IActionResult CambioFecha(string fecha, int idPelicula)
        {
            DateOnly fechaSeleccionada = DateOnly.FromDateTime(DateTime.Parse(fecha).Date);

            var turnos = ObtenerTurnosPorFechaYIdPelicula(fechaSeleccionada, idPelicula);



            return PartialView("_TurnosPartial", turnos);
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
