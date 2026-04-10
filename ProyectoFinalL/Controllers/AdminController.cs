using DataB;
using Modelos;
using ProyectoFinalL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinalL.Controllers
{
    public class AdminController : Controller
    {
        private MathRiddlesDBEntities db = new MathRiddlesDBEntities();


        public ActionResult ManejoAdmin()
        {
            if (Session["EsAdmin"] == null || !(bool)Session["EsAdmin"])
                return RedirectToAction("Index", "Home");

            var model = new AdminViewModel
            {
                Usuarios = db.Usuarios.ToList(),
                Puntajes = db.Puntajes.OrderByDescending(p => p.Puntos).ToList()
            };

            ViewBag.TiempoActual = db.Configuracions.FirstOrDefault(c => c.Clave == "TiempoJuego")?.Valor ?? "30";

            return View(model);
        }

        [HttpPost]
        public ActionResult ReiniciarDashboard()
        {
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE Puntajes");
            return RedirectToAction("ManejoAdmin");
        }

        [HttpPost]
        public ActionResult EliminarPuntaje(int idPuntaje)
        {
            var p = db.Puntajes.Find(idPuntaje);
            if (p != null)
            {
                db.Puntajes.Remove(p);
                db.SaveChanges();
            }
            return RedirectToAction("ManejoAdmin");
        }

        [HttpPost]
        public ActionResult EditarPassword(int idUsuario, string nuevaPassword)
        {
            var user = db.Usuarios.Find(idUsuario);
            if (user != null)
            {
                user.Password = nuevaPassword;
                db.SaveChanges();
            }
            return RedirectToAction("ManejoAdmin");
        }


        [HttpPost]
        public ActionResult CambiarTiempo(string segundos)
        {
            var config = db.Configuracions.FirstOrDefault(c => c.Clave == "TiempoJuego");
            if (config != null)
            {
                config.Valor = segundos;
                db.SaveChanges();
            }
            return RedirectToAction("ManejoAdmin");
        }
    
    [HttpPost]
        public ActionResult ActualizarPuntaje(int idUsuario, int nuevosPuntos)
        {

            var puntaje = db.Puntajes
                            .Where(p => p.IdUsuario == idUsuario)
                            .OrderByDescending(p => p.Puntos)
                            .FirstOrDefault();

            if (puntaje != null)
            {
                puntaje.Puntos = nuevosPuntos;

            }
            else
            {

                db.Puntajes.Add(new Puntaje
                {
                    IdUsuario = idUsuario,
                    Puntos = nuevosPuntos
                });
            }

            db.SaveChanges();
            return RedirectToAction("ManejoAdmin");
        }
    }
}