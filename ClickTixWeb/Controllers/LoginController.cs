using ClickTixWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickTixWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ClicktixContext _context;

        public LoginController(ClicktixContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

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

        public ActionResult Edit(int id)
        {
            return View();
        }

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

        public ActionResult Delete(int id)
        {
            return View();
        }

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

        public ActionResult Login(string email, string password)
        {
            if (UsuarioAutenticado(email, password))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Error"] = "Credenciales incorrectas";
                return View("Index");
            }
        }

        private bool UsuarioAutenticado(string email, string password)
        {
            var usuario = _context.UsuarioWebs.FirstOrDefault(u => u.email == email && u.Pass == password);
            return usuario != null;
        }
    }
}
