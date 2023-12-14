using Microsoft.AspNetCore.Mvc;
using ClickTixWeb.Models;
using FirebaseAdmin.Auth;

public class RegisterController : Controller
{
    private readonly ClicktixContext _context;

    public RegisterController(ClicktixContext context)
    {
        _context = context;
    }

    // GET: RegisterController
    public ActionResult Index()
    {
        return View();
    }

    // GET: RegisterController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: RegisterController/Create





    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAsync(UsuarioWeb model) { 

    {

            FirebaseAuth auth = FirebaseAuth.DefaultInstance;

           
            var nuevoUsuario = await auth.CreateUserAsync(new UserRecordArgs
            {
                Email = model.Email,
                Password = model.Pass,
            });

            Console.WriteLine($"Usuario creado: {nuevoUsuario.Uid}");

            var usuarioWeb = new UsuarioWeb

            {
                
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Pass = model.Pass,
                Email = model.Email,
                Fnac = model.Fnac,
                Genero = model.Genero,
                Celular = model.Celular,
                SucursalHabitual = model.SucursalHabitual
            };

            _context.UsuarioWebs.Add(usuarioWeb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Login");
        }

        return View(model);
    }







    // Otros métodos...

    private bool UsuarioExists(int id)
    {
        return _context.UsuarioWebs.Any(e => e.IdUsuario == id);
    }
}
