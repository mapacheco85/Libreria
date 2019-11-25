using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Libreria.Utilitarios;

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
            if (Membership.ValidateUser(txtUser, txtPwd))
            {
                //En caso de ser positiva la autenticacion.
                FormsAuthentication.RedirectFromLoginPage(txtUser, false);
                //Se debe redirigir a un dashboard.
                return null;

                //return RedirectToAction("Index", "Home");
            }
            else
            {//En caso de no encontrar al usuario.
                return View("Index");
            }
        }



    }
}