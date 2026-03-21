using DataB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{

    public class PuntajeRepository
    {

        public void GuardarPuntaje(Puntaje nuevoPuntaje)
        {
            using (var db = new MathRiddlesDBEntities())
            {
                db.Puntajes.Add(nuevoPuntaje);
                db.SaveChanges();
            }

        }
        public List<Puntaje> ObtenerRanking()
        {
            using (var db = new MathRiddlesDBEntities())
            {

                return db.Puntajes
                         .OrderByDescending(p => p.Puntos)
                         .Take(10)
                         .ToList();
            }
        }
    }
}
