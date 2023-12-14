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
                .Where(usuario => usuario.Email == email)
                .Select(usuario => usuario.IdUsuario)
                .FirstOrDefault();

            return idUsuario;
        }

        public Funcion ObtenerFuncionPorId(int idFuncionTicket)
        {
           
            var funcion = _context.Funcions
                .Where(f => f.Id == idFuncionTicket)
                .FirstOrDefault();

            return funcion;
        }


        [HttpPost]
        public ActionResult ObtenerTickets(string email)
        {
            int idUsuario = ObtenerIdUsuarioPorEmail(email);
            var tickets = ObtenerTicketsDelUsuario(idUsuario);

            // Enviar solo los IDs de las funciones
            var idsFunciones = tickets.Select(ticket => ticket.IdFuncion).ToList();

            return Json(new { Tickets = tickets, IdsFunciones = idsFunciones });
        }



        public List<Funcion> ObtenerFuncionesDeTickets(List<Ticket> tickets)
        {
            
            var idsFunciones = tickets.Select(ticket => ticket.IdFuncion).ToList();

            
            var funciones = _context.Funcions
                .Where(funcion => idsFunciones.Contains(funcion.Id))
                .ToList();

            return funciones;
        }

        public List<Funcion> ObtenerFuncionesPorIds(List<int> idsFunciones)
{
        var funciones = _context.Funcions
            .Where(funcion => idsFunciones.Contains(funcion.Id))
            .ToList();

    return funciones;
}


    }
}
