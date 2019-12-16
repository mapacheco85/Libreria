using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libreria.Models.data;
using Libreria.Utilitarios;

namespace Libreria.Controllers
{
    public class SujetoController : Controller
    {

        // GET: Sujeto
        [Authorize]
        public ActionResult Index()
        {//Se obtienen los datos para pasarlos a la vista.
            using (var DB = new LibreriaDB())
            {
                var Per = DB.PROListarUsuarios().ToList();
                return View(Per);
            }
        }

        [Authorize]
        //Vista Pacial -- PartialView
        public ActionResult Editar(short id)
        {
            using (var DB = new LibreriaDB())
            {
                var Rols = DB.Rol.OrderBy(P => P.Nombre).ToList();
                var Per = DB.Sujeto.SingleOrDefault(P => P.IdSujeto == id);
                var Usu = DB.Usuario.FirstOrDefault(P => P.IdSujeto == Per.IdSujeto);
                var RR = DB.RolUsuario.FirstOrDefault(P => P.IdUsuario == Usu.IdUsuario);
                List<SelectListItem> Roles = new List<SelectListItem>();
                foreach (var Item in Rols)
                {
                    Roles.Add(new SelectListItem()
                    {
                        Value = Item.IdRol.ToString(),
                        Text = Item.Nombre,
                        Selected = Item.IdRol == RR.IdRol ? true : false
                    });
                }
                ViewData["Roles"] = Roles;
                ViewData["Login"] = Usu.Login;
                ViewData["Pwd"] = Usu.Pwd;

                return PartialView(Per);
            }
        }

        [Authorize]
        //Vista Parcial -- PartialView
        public ActionResult Agregar()
        {
            using (var DB = new LibreriaDB())
            {

                var Rols = DB.Rol.OrderBy(P => P.Nombre).ToList();
                List<SelectListItem> Roles = new List<SelectListItem>();
                Roles.Add(new SelectListItem() { Value = "0", Text = "Seleccione Rol", Selected = true });
                foreach (var Item in Rols)
                {
                    Roles.Add(new SelectListItem()
                    {
                        Value = Item.IdRol.ToString(),
                        Text = Item.Nombre,
                        Selected = false
                    });
                }
                ViewData["Roles"] = Roles;
                return PartialView();
            }

        }

        //POST: Funciones.
        [HttpPost]
        [Authorize]
        public ActionResult Editar(short id, FormCollection Col)
        {
            if (Col.Count > 0)
            {
                using (var DB = new LibreriaDB())
                {
                    var Su = DB.Sujeto.SingleOrDefault(P => P.IdSujeto == id);
                    Su.Nombres = Col["txtNombres"];
                    Su.Apellidos = Col["txtApellidos"];
                    Su.CI_NIT = Col["txtCiNit"];
                    Su.Telefonos = Col["txtTelefonos"];
                    Su.Direccion = Col["txtDireccion"];
                    Su.Email = Col["txtEmail"];
                    Su.RazonSocial = Col["txtRazonSocial"];
                    //Editar el Usuario (Login, Pwd)
                    var Usu = DB.Usuario.FirstOrDefault(P => P.IdSujeto == Su.IdSujeto);
                    Usu.Login = Col["txtLogin"];

                    if (Usu.Pwd != Col["txtPwd"])
                        Usu.Pwd = Libreria.Utilitarios.Utils.GetSha1(Col["txtPwd"]);
                    else
                        Usu.Pwd = Col["txtPwd"];
                    //Editar el Rol
                    var Rols = DB.RolUsuario.SingleOrDefault(P => P.IdUsuario == Usu.IdUsuario);
                    Rols.IdRol = short.Parse(Col["IdRol"]);


                    DB.SaveChanges();

                    var Listado = DB.PROListarUsuarios().ToList();
                    return Redirect("/Libreria/sujeto/");
                    //return RedirectToAction("Index", Listado);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Agregar(FormCollection Col)
        {
            if (Col.Count > 0)
            {
                //Validaciones de la data.
                //Registro en la base de datos.
                using (var DB = new LibreriaDB())
                {
                    Sujeto Su = new Sujeto();
                    Su.Nombres = Col["txtNombres"];
                    Su.Apellidos = Col["txtApellidos"];
                    Su.CI_NIT = Col["txtCiNit"];
                    Su.Telefonos = Col["txtTelefonos"];
                    Su.Direccion = Col["txtDireccion"];
                    Su.Email = Col["txtEmail"];
                    Su.RazonSocial = Col["txtRazonSocial"];
                    //Se agrega el Usuario.
                    Usuario Us = new Usuario();
                    Us.IdClasificacion = 1;
                    Us.Login = Col["txtLogin"];
                    Us.Pwd = Libreria.Utilitarios.Utils.GetSha1(Col["txtPwd"]);
                    Us.Activo = true;
                    //Se agrega en RolUsuario
                    RolUsuario RolUsu = new RolUsuario();
                    RolUsu.IdSucursal = 1;
                    RolUsu.IdRol = short.Parse(Col["IdRol"]);
                    RolUsu.FechaREG = DateTime.Now;
                    RolUsu.Activo = true;

                    Us.RolUsuario.Add(RolUsu);
                    Su.Usuario.Add(Us);
                    DB.Sujeto.Add(Su);

                    DB.SaveChanges();

                    var ListaSu = DB.PROListarUsuarios().ToList();
                    return Redirect("/Libreria/sujeto/");
                    //return View("Index", ListaSu);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Eliminar(short id)
        {

            using (var DB = new LibreriaDB())
            {
                Usuario u = DB.Usuario.SingleOrDefault(U => U.IdSujeto == id);

                RolUsuario ru = DB.RolUsuario.SingleOrDefault(R => R.IdUsuario == u.IdUsuario);
                DB.RolUsuario.Remove(ru);
                DB.SaveChanges();

                DB.Usuario.Remove(u);
                DB.SaveChanges();

                Sujeto obj = DB.Sujeto.SingleOrDefault(P => P.IdSujeto == id);
                DB.Sujeto.Remove(obj);

                DB.SaveChanges();

                var Prods = DB.Sujeto.OrderBy(P => P.Nombres).ToList();
                return Redirect("/Libreria/sujeto/");
            }
        }
    }
}