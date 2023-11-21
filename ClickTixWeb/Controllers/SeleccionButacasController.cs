using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClickTixWeb.Controllers
{
    public class SeleccionButacasController : Controller
    {
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
    }
}
