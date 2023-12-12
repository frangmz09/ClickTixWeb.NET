using ClickTixWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickTixWeb.Controllers
{
    public class TusTicketsController : Controller
    {

        private readonly ClicktixContext _context;



        public TusTicketsController(ClicktixContext context)
        {
            _context = context;

        }



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


        [HttpPost]
        public ActionResult ObtenerTickets(string email)
        {

            int idUsuario =  ObtenerIdUsuarioPorEmail(email);


            var tickets = ObtenerTicketsDelUsuario(9);

            return Json(tickets);
        }


        public List<Ticket> ObtenerTicketsDelUsuario(int id)
        {
            var tickets = _context.Tickets
                .Where(ticket => ticket.IdUsuario == id)
                .ToList();

            return tickets;
        }

        public int ObtenerIdUsuarioPorEmail(string email)
        {
            
            var idUsuario = _context.UsuarioWebs
                .Where(usuario => usuario.email == email)
                .Select(usuario => usuario.IdUsuario)
                .FirstOrDefault();

            return idUsuario;
        }



    }
}
