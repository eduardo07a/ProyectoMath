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

            return View();
        }

        [HttpPost]
        public JsonResult GuardarPuntaje(int puntos, string dificultad)
        {
            try
            {
                int idUsuario = (int)Session["UsuarioId"];

                var nuevoPuntaje = new DataB.Puntaje
                {
                    IdUsuario = idUsuario,
                    Puntos = puntos,
                    Dificultad = dificultad
                };


                _repoPuntaje.GuardarPuntaje(nuevoPuntaje);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}