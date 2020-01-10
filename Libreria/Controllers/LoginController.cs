using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Libreria.Utilitarios;
using Libreria.Models.data;

namespace Libreria.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        // POST: Resive los datos.
        [HttpPost]
        public ActionResult Index(string txtUser, string txtPwd)
        {
            int cont = 0;
            LibreriaDB DB = new LibreriaDB();
            Usuario usu = DB.Usuario.FirstOrDefault(P => P.Login == txtUser);
            if (usu != null)
                cont = usu.intento;
            if (cont < 3 && Membership.ValidateUser(txtUser, txtPwd))
            {
                //En caso de ser positiva la autenticacion.
                FormsAuthentication.RedirectFromLoginPage(txtUser, false);
                FormsAuthentication.SetAuthCookie(txtUser, false);
                //Se debe redirigir a un dashboard.
                ViewBag.Mensaje = "";
                return null;

                //return RedirectToAction("Index", "Home");
            }
            else
            {   //En caso de no encontrar al usuario.
                if (usu != null)
                {
                    usu.intento = usu.intento + 1;
                    DB.SaveChanges();
                }
                else
                    ViewBag.Mensaje = "Usuario Incorrecto!!";
                ViewBag.Mensaje = "Contrasena Incorrecta!!";
                if (cont>=3)
                    ViewBag.Mensaje = "Cuenta bloqueada!!";
                return View("Index");
            }
        }
    }
}