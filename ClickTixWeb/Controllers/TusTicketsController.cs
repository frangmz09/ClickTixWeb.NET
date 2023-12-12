using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClickTixWeb.Controllers
{
    public class TusTicketsController : Controller
    {


        // GET: TusTicketsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TusTicketsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TusTicketsController/Create
        public ActionResult Create()

        {

            return View();
        }

        // POST: TusTicketsController/Create
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

        // GET: TusTicketsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TusTicketsController/Edit/5
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

        // GET: TusTicketsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TusTicketsController/Delete/5
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
