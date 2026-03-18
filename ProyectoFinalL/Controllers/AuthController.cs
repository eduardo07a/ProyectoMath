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
                Session["Usuario"] = user.Correo;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuario incorrecto";
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string correo, string password)
        {
            var nuevo = new Usuario
            {
                Correo = correo,
                Password = password
            };
            bool exito = _repo.RegistrarUsuario(nuevo);

            if (exito)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Error = "El correo ya está registrado o los datos son inválidos.";
            return View();

        }
    }
  }

        
