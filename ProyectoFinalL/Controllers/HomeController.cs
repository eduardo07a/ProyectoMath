using DataB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinalL.Controllers
{
    public class HomeController : Controller
    {
        MathRiddlesDBEntities db = new MathRiddlesDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SeleccionarNivel()
        {
            if (Session["UsuarioId"] == null) return RedirectToAction("Login", "Auth");
            return View();
        }

        public ActionResult Ranking(string buscarNombre, string filtroDificultad)
        {
            ViewBag.Usuarios = db.Usuarios.ToList();
            var consulta = db.Puntajes.AsQueryable();

            if (!string.IsNullOrEmpty(filtroDificultad) && filtroDificultad != "Todos")
            {
                consulta = consulta.Where(p => p.Dificultad == filtroDificultad);
            }

            if (!string.IsNullOrEmpty(buscarNombre))
            {
                var idsUsuarios = db.Usuarios
                    .Where(u => u.Nombre.Contains(buscarNombre))
                    .Select(u => u.IdUsuario).ToList();

                if (idsUsuarios.Count == 0)
                {
                    ViewBag.ErrorBusqueda = "ESTE JUGADOR NO EXISTE";
                    ViewBag.NombreLimpio = ""; 
                    return View(new List<DataB.Puntaje>());
                }

                consulta = consulta.Where(p => idsUsuarios.Contains((int)p.IdUsuario));
                ViewBag.NombreLimpio = buscarNombre; 
            }

            var resultados = consulta.OrderByDescending(p => p.Puntos).Take(10).ToList();
            return View(resultados);
        }
    }
}