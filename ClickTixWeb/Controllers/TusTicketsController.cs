using ClickTixWeb.Models;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickTixWeb.Controllers
{
    public class TusTicketsController : Controller
    {

        private readonly ClicktixContext _context;

        FirebaseAuth auth = FirebaseAuth.DefaultInstance;


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





        public List<Ticket> ObtenerTicketsDelUsuario(string id)
        {
            var tickets = _context.Tickets
                .Where(ticket => ticket.UidFb.Equals(id))
                .ToList();

            Console.WriteLine($"Número total de tickets del usuario: {tickets.Count}");

            for (int i = 0; i < tickets.Count; i++)
            {
                Console.WriteLine($"Ticket {i + 1}: {tickets[i].Id}");
            }

            return tickets;
        }




        
        public string ObtenerIdUsuarioPorEmail(string email)
        {
            var userRecord = auth.GetUserByEmailAsync(email).Result;

            return userRecord.Uid;

        }


        [HttpPost]
        public ActionResult ObtenerTickets(string email)
        {
            string idUsuario = ObtenerIdUsuarioPorEmail(email);
            var tickets = ObtenerTicketsDelUsuario(idUsuario);

            if (tickets != null && tickets.Any())
            {
                var idsFunciones = tickets.Select(ticket => ticket.IdFuncion).ToList();

                for (int i = 0; i < idsFunciones.Count; i++)
                {
                    Console.WriteLine($"FUNCION {i + 1}: {idsFunciones[i]} ");
                }


                // Obtener las funciones como strings
                var funcionesStrings = ListarFunciones(idsFunciones);


                for (int i = 0; i < funcionesStrings.Count; i++)
                {
                    Console.WriteLine($"FUCNION STRING {i + 1}: {funcionesStrings[i]} ");
                }


                Console.WriteLine($"FUNCIONES STRINGS: {funcionesStrings}");
                if (funcionesStrings != null && funcionesStrings.Any())
                {
                    TusTicketsViewModel ttvm = new TusTicketsViewModel(funcionesStrings);

                    Console.WriteLine($"TTVM :  {ttvm.funcionesStrings[0].Pelicula}");
                    Console.WriteLine($"TTVM :  {ttvm.funcionesStrings[0].Idioma}");


                    

                    return PartialView("~/Views/TusTickets/_PartialLayout.cshtml", ttvm);

                    
                }
            }

            // Manejo de caso donde no hay tickets o funcionesStrings
            return Content("No se encontraron tickets para el usuario");
        }



        public List<FuncionStrings> ListarFunciones(List<int> idsFunciones)
        {
            List<FuncionStrings> funcionesStrings = new List<FuncionStrings>();

            foreach (var idFuncion in idsFunciones)
            {
                var funcion = ObtenerFuncionStrings(idFuncion);



                if (funcion != null)
                {
                    funcionesStrings.Add(funcion);
                }
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
                                      Id = f.Id

                                  }).FirstOrDefault();



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
