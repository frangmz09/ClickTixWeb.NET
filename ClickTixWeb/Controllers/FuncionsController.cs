using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClickTixWeb.Models;

namespace ClickTixWeb.Controllers
{
    public class FuncionsController : Controller
    {
        private readonly ClicktixContext _context;

        public FuncionsController(ClicktixContext context)
        {
            _context = context;
        }

        // GET: Funcions
        public async Task<IActionResult> Index()
        {
            var clicktixContext = _context.Funcions.Include(f => f.IdDimensionNavigation).Include(f => f.IdPeliculaNavigation).Include(f => f.IdSalaNavigation).Include(f => f.Turno);
            return View(await clicktixContext.ToListAsync());
        }

        // GET: Funcions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funcions == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcions
                .Include(f => f.IdDimensionNavigation)
                .Include(f => f.IdPeliculaNavigation)
                .Include(f => f.IdSalaNavigation)
                .Include(f => f.Turno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // GET: Funcions/Create
        public IActionResult Create()
        {
            ViewData["IdDimension"] = new SelectList(_context.Dimensions, "Id", "Id");
            ViewData["IdPelicula"] = new SelectList(_context.Peliculas, "Id", "Id");
            ViewData["IdSala"] = new SelectList(_context.Salas, "Id", "Id");
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "Id");
            return View();
        }

        // POST: Funcions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,IdDimension,IdPelicula,IdSala,IdiomaFuncion,TurnoId")] Funcion funcion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDimension"] = new SelectList(_context.Dimensions, "Id", "Id", funcion.IdDimension);
            ViewData["IdPelicula"] = new SelectList(_context.Peliculas, "Id", "Id", funcion.IdPelicula);
            ViewData["IdSala"] = new SelectList(_context.Salas, "Id", "Id", funcion.IdSala);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "Id", funcion.TurnoId);
            return View(funcion);
        }

        // GET: Funcions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funcions == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcions.FindAsync(id);
            if (funcion == null)
            {
                return NotFound();
            }
            ViewData["IdDimension"] = new SelectList(_context.Dimensions, "Id", "Id", funcion.IdDimension);
            ViewData["IdPelicula"] = new SelectList(_context.Peliculas, "Id", "Id", funcion.IdPelicula);
            ViewData["IdSala"] = new SelectList(_context.Salas, "Id", "Id", funcion.IdSala);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "Id", funcion.TurnoId);
            return View(funcion);
        }

        // POST: Funcions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,IdDimension,IdPelicula,IdSala,IdiomaFuncion,TurnoId")] Funcion funcion)
        {
            if (id != funcion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionExists(funcion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDimension"] = new SelectList(_context.Dimensions, "Id", "Id", funcion.IdDimension);
            ViewData["IdPelicula"] = new SelectList(_context.Peliculas, "Id", "Id", funcion.IdPelicula);
            ViewData["IdSala"] = new SelectList(_context.Salas, "Id", "Id", funcion.IdSala);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "Id", funcion.TurnoId);
            return View(funcion);
        }

        // GET: Funcions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funcions == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcions
                .Include(f => f.IdDimensionNavigation)
                .Include(f => f.IdPeliculaNavigation)
                .Include(f => f.IdSalaNavigation)
                .Include(f => f.Turno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // POST: Funcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funcions == null)
            {
                return Problem("Entity set 'ClicktixContext.Funcions'  is null.");
            }
            var funcion = await _context.Funcions.FindAsync(id);
            if (funcion != null)
            {
                _context.Funcions.Remove(funcion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionExists(int id)
        {
          return (_context.Funcions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
