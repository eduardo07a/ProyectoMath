using System.Web.Mvc;
using Repositorios;
using DataB;
using System.Linq;

namespace ProyectoFinalL.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioRepository _repo = new UsuarioRepository();
        MathRiddlesDBEntities db = new MathRiddlesDBEntities();

        public ActionResult Perfil()
        {
            if (Session["UsuarioId"] == null) return RedirectToAction("Login", "Auth");

            int idUsuario = (int)Session["UsuarioId"];
            var usuario = db.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);

            ViewBag.MejorPuntaje = db.Puntajes
                                     .Where(p => p.IdUsuario == idUsuario)
                                     .Max(p => (int?)p.Puntos) ?? 0;

            return View(usuario);
        }

        [HttpPost]
        public ActionResult GuardarPerfil(string nuevoNombre)
        {
            if (Session["UsuarioId"] == null) return RedirectToAction("Login", "Auth");

            int idActual = (int)Session["UsuarioId"];

            bool existe = db.Usuarios.Any(u => u.Nombre == nuevoNombre && u.IdUsuario != idActual);

            var usuarioDB = db.Usuarios.FirstOrDefault(u => u.IdUsuario == idActual);

            if (existe)
            {

                ViewBag.Error = "El nombre '" + nuevoNombre + "' ya está en uso por otro jugador.";

                ViewBag.MejorPuntaje = db.Puntajes.Where(p => p.IdUsuario == idActual).Max(p => (int?)p.Puntos) ?? 0;

                return View("Perfil", usuarioDB); 
            }

            if (usuarioDB != null)
            {
                usuarioDB.Nombre = nuevoNombre;
                db.SaveChanges();
                Session["Usuario"] = nuevoNombre; 
            }

            return RedirectToAction("Perfil");
        }

        [HttpPost]
        public ActionResult CambiarContrasena(string nuevaContrasena, string confirmarContrasena)
        {
            if (Session["UsuarioId"] == null) return RedirectToAction("Login", "Auth");

            if (nuevaContrasena != confirmarContrasena)
            {
                ViewBag.Error = "¡Las contraseñas no coinciden!";
                int id = (int)Session["UsuarioId"];
                var usuario = db.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
                return View("Perfil", usuario);
            }

            int idUsuario = (int)Session["UsuarioId"];
            var usuarioDB = db.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);

            if (usuarioDB != null)
            {

                usuarioDB.Password = nuevaContrasena;
                db.SaveChanges();

                ViewBag.Success = "¡Contraseña actualizada con éxito!";
                return RedirectToAction("Perfil");
            }

            return View("Perfil");
        }

    }
}