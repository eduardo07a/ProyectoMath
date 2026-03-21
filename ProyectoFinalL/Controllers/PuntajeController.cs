using DataB;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ProyectoFinalL.Controllers
{
    public class PuntajeController : Controller
    {
        PuntajeRepository _repo = new PuntajeRepository();

        public ActionResult Ranking()
        {
            using (var db = new MathRiddlesDBEntities())
            {
                var listaPuntajes = db.Puntajes
                             .Where(p => p.Puntos > 0)
                             .OrderByDescending(p => p.Puntos)
                             .Take(10)
                             .ToList();

                ViewBag.Usuarios = db.Usuarios.ToList();
                return View(listaPuntajes);
            }
        }
    }
}