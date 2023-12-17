using ClickTixWeb.Models;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickTixWeb.Controllers
{
    public class CompraController : Controller
    {
        private readonly ClicktixContext _context;
        private readonly ILogger<DetalleController> _logger;
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;


        public CompraController(ILogger<DetalleController> logger, ClicktixContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpPost]
        public ActionResult ConfirmarCompra(IFormCollection formCollection)
        {
            var funcionId = int.Parse(formCollection["funcionId"]);
            var precioMomento = decimal.Parse(formCollection["precioMomento"]);
            var email = formCollection["emailHidden"];
            var asientosIds = formCollection["asientosId"].Select(id => int.Parse(id)).ToList();
            string UserId = ObtenerUid(email);

            foreach (var asientoId in asientosIds)
            {
                var asiento = _context.Asientos.SingleOrDefault(a => a.Id == asientoId);

                if (asiento != null)
                {
                    asiento.Disponible = 0;
                }
            }

            _context.SaveChanges();
            foreach (var idAsiento in asientosIds)
            {
                DateTime fechaActual = DateTime.Now;
                var nuevoTicket = new Ticket
                    {
                        IdFuncion = funcionId,
                        Fecha = fechaActual,
                        Fila = ObtenerFilaPorId(idAsiento), 
                        Columna = ObtenerColumnaPorId(idAsiento), 
                        PrecioAlMomento = (double)precioMomento,
                        UidFb = UserId,
                    };
                    _context.Tickets.Add(nuevoTicket);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Home"); 
        }
        public int ObtenerFilaPorId(int asientoId)
        {
            var fila = _context.Asientos
                .Where(asiento => asiento.Id == asientoId)
                .Select(asiento => asiento.Fila)
                .FirstOrDefault();

            return (int)fila;
        }
        public int ObtenerColumnaPorId(int asientoId)
        {
            var fila = _context.Asientos
                .Where(asiento => asiento.Id == asientoId)
                .Select(asiento => asiento.Columna)
                .FirstOrDefault();

            return (int)fila;
        }
        public string ObtenerUid(string email)
        {
            var userRecord = auth.GetUserByEmailAsync(email).Result;

            return userRecord.Uid;

        }

        // GET: CompraController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompraController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompraController/Create
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

        // GET: CompraController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompraController/Edit/5
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

        // GET: CompraController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompraController/Delete/5
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
