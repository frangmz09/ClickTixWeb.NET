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

            var idsFunciones = tickets.Select(ticket => ticket.IdFuncion).ToList();

         

            // Obtener las funciones como strings
            var funcionesStrings = ListarFunciones(idsFunciones);


            TusTicketsViewModel ttvm = new TusTicketsViewModel(funcionesStrings);


            return View("~/Views/TusTickets/Index.cshtml", funcionesStrings);

            //return Json(new { Tickets = tickets, FuncionesStrings = funcionesStrings });
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

        public List<FuncionStrings> ListarFunciones(List<int> idsFunciones)
        {

            List<FuncionStrings> funcionesStrings = new List<FuncionStrings>();
            for (int i = 0; i < idsFunciones.Count; i++)
            {
                var funcion = ObtenerFuncionStrings(idsFunciones[i]);

                funcionesStrings.Add(funcion);
                
            }

            return funcionesStrings;


        }



        public FuncionStrings ObtenerFuncionStrings(int funcionIdABuscar)
        {
            var funcionStrings = (from f in _context.Funcions
                                  join pelicula in _context.Peliculas on f.IdPelicula equals pelicula.Id
                                  join dimension in _context.Dimensions on f.IdDimension equals dimension.Id
                                  join sala in _context.Salas on f.IdSala equals sala.Id
                                  join turno in _context.Turnos on f.TurnoId equals turno.Id
                                  join sucursal in _context.Sucursals on sala.IdSucursal equals sucursal.Id
                                  where f.Id == funcionIdABuscar
                                  select new FuncionStrings
                                  {
                                      Dimension = dimension.Dimension1,
                                      Pelicula = pelicula.Titulo,
                                      Sala = sala.NroSala.ToString(),
                                      Turno = turno.Hora.ToString(),
                                      Idioma = f.IdiomaFuncion,
                                      Sucursal = sucursal.Nombre,
                                      CuitSucursal = sucursal.Cuit.ToString(),
                                      precioFuncion = (double)dimension.Precio,
                                      Fecha = f.Fecha.ToString(),
                                     
                                      
                                  }).FirstOrDefault();


            funcionStrings.Idioma = ObtenerNombreIdioma(funcionStrings.Idioma);

            return funcionStrings;
        }
        public string ObtenerNombreIdioma(string idioma)
        {

            int idiomaId = int.Parse(idioma);

            var nombreIdioma = _context.Idiomas
                .Where(idioma => idioma.Id == idiomaId)
                .Select(idioma => idioma.Idioma1)
                .FirstOrDefault() ?? "Sin idioma";

            return nombreIdioma;
        }




    }
}
