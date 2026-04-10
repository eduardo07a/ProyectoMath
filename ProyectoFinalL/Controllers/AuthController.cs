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

       
            ViewBag.Error = "USUARIO O CONTRASEÑA INCORRECTOS";
            ViewBag.Modo = "login";
            return View();
        }

        [HttpPost]
        public ActionResult Register(string correo, string password, string nombre)
        {
            
            var existe = db.Usuarios.Any(u => u.Correo == correo);

            if (existe)
            {
                ViewBag.Error = "ESTE CORREO YA ESTÁ REGISTRADO";
                ViewBag.Modo = "registro";
                return View("Login"); 
            }

            var nuevo = new Usuario
            {
                Correo = correo,
                Password = password,
                Nombre = nombre,
                EsAdmin = false
            };

            bool exito = _repo.RegistrarUsuario(nuevo);
            if (exito) return RedirectToAction("Login");

            ViewBag.Error = "ERROR AL REGISTRAR. INTENTA DE NUEVO.";
            ViewBag.Modo = "registro";
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}