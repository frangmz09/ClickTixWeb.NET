using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickTixWeb.Models;
using System.Diagnostics;
using FirebaseAdmin.Auth;
using FirebaseAdmin;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,Apellido,Pass,Passc,email,fnac,genero,celular,sucursalHabitual")] UsuarioWeb usuarioWeb)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,Apellido,Pass,Passc,email,fnac,genero,celular,sucursalHabitual")] UsuarioWeb usuarioWeb)
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

        // GET: UsuarioWebs/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: UsuarioWebs/Login
        [HttpPost]
        [ValidateAntiForgeryToken]



        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewData["Error"] = "Ingrese correo electrónico y contraseña";
                return View("Login");
            }

            if (UsuarioAutenticado(email, password))
            {
                Trace.WriteLine("USUARIO EXISTEEEEADFASFASFASFSAFASFAFWQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                HttpContext.Session.SetString("UsuarioEmail", email);

                string nombreUsuario = ObtenerNombreUsuario(email);
                HttpContext.Session.SetString("UsuarioNombre", nombreUsuario);

                ViewData["UsuarioNombre"] = nombreUsuario;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Error"] = "Credenciales incorrectas";
                return View("Login");
            }
        }

        private string ObtenerNombreUsuario(string email)
        {
            var usuario = _context.UsuarioWebs.FirstOrDefault(u => u.Email == email);
            return usuario != null ? $"{usuario.Nombre} {usuario.Apellido}" : string.Empty;
        }
        public IActionResult CerrarSesion()
        {
            // Elimina todos los valores de la sesión
            HttpContext.Session.Clear();

            // O específicamente para un valor:
            // HttpContext.Session.Remove("UsuarioEmail");

            // Redirige a donde sea necesario después de cerrar sesión
            return RedirectToAction("Index", "Home");
        }

        private bool UsuarioWebExists(int id)
        {
            return (_context.UsuarioWebs?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }

        private bool UsuarioAutenticado(string email, string password)
        {
            var usuario = _context.UsuarioWebs.FirstOrDefault(u => u.Email == email && u.Pass == password);

            if (usuario != null)
            {
                Console.WriteLine($"Autenticación para {email}: {usuario.Nombre} AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                return true;
            }

            return false;
        }






    }
}