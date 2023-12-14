using ClickTixWeb.Models;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using FirebaseAdmin.Auth;

namespace ClickTixWeb.Controllers
{
    public class LoginController : Controller
    {

        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        private readonly ClicktixContext _context;
        private readonly IConfiguration _configuration;
        public LoginController(ClicktixContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

        public string IniciarSesion(string email)
        {
            var uid = ObtenerUidDeUsuario(email); 
            var token = CrearTokenPersonalizado(uid);

            return token;
        }

        private string CrearTokenPersonalizado(Task<UserRecord> uid)
        {
            return "sesion" + uid.ToString();
        }

        private Task<UserRecord> ObtenerUidDeUsuario(string email)
        {
            return auth.GetUserByEmailAsync(email);

        }

        public ActionResult Login(string email, string password)
        {

           


            if (UsuarioAutenticado(email, password))
            {

     
                ViewData["Token"] = email;
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



            var usuario = _context.UsuarioWebs.FirstOrDefault(u => u.Email == email && u.Pass == password);
            return usuario != null;
        }


       

       
           
        




        public void Metodo()
        {
            var apiKey = _configuration["Firebase:ApiKey"];
            var authDomain = _configuration["Firebase:AuthDomain"];
            var privateKey = _configuration["Firebase:PrivateKey"];

            var credentials = GoogleCredential.FromJson(privateKey);

           
        }

    }
}
