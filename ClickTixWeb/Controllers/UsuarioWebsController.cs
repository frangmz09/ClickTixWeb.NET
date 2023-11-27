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
    public class UsuarioWebsController : Controller
    {
        private readonly ClicktixContext _context;

        public UsuarioWebsController(ClicktixContext context)
        {
            _context = context;
        }

        // GET: UsuarioWebs
        public async Task<IActionResult> Index()
        {
              return _context.UsuarioWebs != null ? 
                          View(await _context.UsuarioWebs.ToListAsync()) :
                          Problem("Entity set 'ClicktixContext.UsuarioWebs'  is null.");
        }

        // GET: UsuarioWebs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuarioWebs == null)
            {
                return NotFound();
            }

            var usuarioWeb = await _context.UsuarioWebs
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuarioWeb == null)
            {
                return NotFound();
            }

            return View(usuarioWeb);
        }

        // GET: UsuarioWebs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsuarioWebs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,Apellido,Pass")] UsuarioWeb usuarioWeb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioWeb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioWeb);
        }

        // GET: UsuarioWebs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioWebs == null)
            {
                return NotFound();
            }

            var usuarioWeb = await _context.UsuarioWebs.FindAsync(id);
            if (usuarioWeb == null)
            {
                return NotFound();
            }
            return View(usuarioWeb);
        }

        // POST: UsuarioWebs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,Apellido,Pass")] UsuarioWeb usuarioWeb)
        {
            if (id != usuarioWeb.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioWeb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioWebExists(usuarioWeb.IdUsuario))
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
            return View(usuarioWeb);
        }

        // GET: UsuarioWebs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuarioWebs == null)
            {
                return NotFound();
            }

            var usuarioWeb = await _context.UsuarioWebs
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuarioWeb == null)
            {
                return NotFound();
            }

            return View(usuarioWeb);
        }

        // POST: UsuarioWebs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuarioWebs == null)
            {
                return Problem("Entity set 'ClicktixContext.UsuarioWebs'  is null.");
            }
            var usuarioWeb = await _context.UsuarioWebs.FindAsync(id);
            if (usuarioWeb != null)
            {
                _context.UsuarioWebs.Remove(usuarioWeb);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioWebExists(int id)
        {
          return (_context.UsuarioWebs?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
