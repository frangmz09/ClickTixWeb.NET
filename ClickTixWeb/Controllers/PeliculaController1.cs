using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClickTixWeb.Controllers
{
    public class PeliculaController1 : Controller
    {
        // GET: PeliculaController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: PeliculaController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PeliculaController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PeliculaController1/Create
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

        // GET: PeliculaController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PeliculaController1/Edit/5
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

        // GET: PeliculaController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PeliculaController1/Delete/5
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
