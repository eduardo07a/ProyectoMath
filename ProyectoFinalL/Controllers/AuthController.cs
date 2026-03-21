using System.Linq;
using System.Web.Mvc;
using DataB;
using Repositorios;

namespace ProyectoFinalL.Controllers
{
    public class AuthController : Controller
    {
        MathRiddlesDBEntities db = new MathRiddlesDBEntities();
        UsuarioRepository _repo = new UsuarioRepository();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string correo, string password)
        {
            var user = db.Usuarios
                .FirstOrDefault(x => x.Correo == correo && x.Password == password);

            if (user != null)
            {
                Session["Usuario"] = user.Nombre; 
                Session["UsuarioId"] = user.IdUsuario;
                Session["EsAdmin"] = user.EsAdmin;

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string correo, string password, string nombre)
        {
            var nuevo = new Usuario
            {
                Correo = correo,
                Password = password,
                Nombre = nombre, 
                EsAdmin = false 
            };

            bool exito = _repo.RegistrarUsuario(nuevo);
            if (exito) return RedirectToAction("Login");

            ViewBag.Error = "Error al registrar. Intenta con otro correo.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}