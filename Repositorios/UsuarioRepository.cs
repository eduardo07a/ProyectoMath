using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataB;

namespace Repositorios
{
    public class UsuarioRepository
    {
        public bool RegistrarUsuario(Usuario nuevoUsuario)
        {
            using (var db = new MathRiddlesDBEntities())
            {
               
                if (db.Usuarios.Any(u => u.Correo == nuevoUsuario.Correo))
                {
                    return false;
                }

                db.Usuarios.Add(nuevoUsuario);
                db.SaveChanges();
                return true;
            }
        }
    }
}
