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
        public bool RegistrarUsuario(Usuario nuevo)
        {
            using (var db = new MathRiddlesDBEntities())
            {

                bool existeNombre = db.Usuarios.Any(x => x.Nombre == nuevo.Nombre);


                bool existeCorreo = db.Usuarios.Any(x => x.Correo == nuevo.Correo);

                if (existeNombre || existeCorreo)
                {
                    return false;
                }

                db.Usuarios.Add(nuevo);
                db.SaveChanges();
                return true;
            }
        }
        public bool ActualizarNombre(int idUsuario, string nuevoNombre)
        {
            using (var db = new MathRiddlesDBEntities())
            {

                bool yaExisteEseNombre = db.Usuarios
                    .Any(x => x.Nombre == nuevoNombre && x.IdUsuario != idUsuario);

                if (yaExisteEseNombre)
                {
                    return false;
                }

                var usuario = db.Usuarios.FirstOrDefault(x => x.IdUsuario == idUsuario);
                if (usuario != null)
                {
                    usuario.Nombre = nuevoNombre;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
