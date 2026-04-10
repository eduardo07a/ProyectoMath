using DataB;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinalL.Controllers
{
    public class GameController : Controller
    {
        private MathRiddlesDBEntities db = new MathRiddlesDBEntities();
        PuntajeRepository _repoPuntaje = new PuntajeRepository();


        public ActionResult Index(string nivel)
        {
            ViewBag.Nivel = nivel;


            if (Session["UsuarioId"] != null)
            {
                int id = (int)Session["UsuarioId"];

                var usuario = db.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

                if (usuario != null)
                {
                    ViewBag.NombreUsuario = usuario.Nombre;
                }
            }
            else
            {
                ViewBag.NombreUsuario = "Error: No hay sesión";
            }
            var tiempoConfig = db.Configuracions.FirstOrDefault(c => c.Clave == "TiempoJuego")?.Valor ?? "30";
            ViewBag.TiempoLimite = tiempoConfig;
            ViewBag.Nivel = nivel;
            return View();

        }

        [HttpPost]
        public JsonResult GuardarPuntaje(int puntos, string dificultad)
        {
            if (Session["UsuarioId"] == null)
            {
                return Json(new { success = false, message = "Sesión expirada" });
            }

            try
            {
                int idUsuario = (int)Session["UsuarioId"];

                using (var db = new MathRiddlesDBEntities())
                {
                    var nuevoRegistro = new Puntaje
                    {
                        IdUsuario = idUsuario,
                        Puntos = puntos,
                        Dificultad = dificultad

                    };

                    db.Puntajes.Add(nuevoRegistro);
                    db.SaveChanges();
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

    }
}
