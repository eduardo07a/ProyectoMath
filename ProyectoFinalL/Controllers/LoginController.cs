using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace ProyectoFinalL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Login(string correo, string password)
        {
            string conexion = ConfigurationManager.ConnectionStrings["MathRiddlesDBEntities"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conexion))
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Correo=@correo AND PasswordHash=@password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@password", password);

                con.Open();
                int existe = (int)cmd.ExecuteScalar();

                if (existe > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Usuario incorrecto";
                    return View(); 
                }
            }
        }
    }
}