using Libreria.Models.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        [Authorize]
        public ActionResult Index()
        {
            using (var DB = new ModLibreriaDB())
            {
                DB.Configuration.LazyLoadingEnabled = false;
                var Cli = DB.Cliente.OrderBy(C => C.Nombre);

                return View(Cli.ToList());
            }
        }

        [Authorize]
        public ActionResult Agregar()
        {
            using (var DB = new ModLibreriaDB())
            {
                var Cat = DB.Categoria.OrderBy(P => P.Nombre).ToList();
                return PartialView(Cat);
            }
        }

        [Authorize]
        public ActionResult Editar(short id)
        {
            using (var DB = new ModLibreriaDB())
            {
                var Cli = DB.Cliente.SingleOrDefault(P => P.IdCliente == id);
                return PartialView(Cli);
            }
        }

        //POST:
        [HttpPost]
        [Authorize]
        public ActionResult Agregar(FormCollection Col)
        {
            if (Col.Count > 0)
            {
                using (var DB = new ModLibreriaDB())
                {
                    Cliente Cli = new Cliente();
                    Cli.Nombre = Col["txtNombre"];
                    Cli.NIT = Col["txtNit"];
                    Cli.RazonSocial = Col["txtNombre"];
                    Cli.Telefonos = Col["txtTelefonos"];
                    Cli.Email = Col["txtEmail"];
                    DB.Cliente.Add(Cli);
                    DB.SaveChanges();

                    return Redirect("/Libreria/cliente/");
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Editar(short id, FormCollection Col)
        {
            if (Col.Count > 0)
            {
                using (var DB = new ModLibreriaDB())
                {
                    Cliente Cli = DB.Cliente.SingleOrDefault(P => P.IdCliente == id);
                    Cli.Nombre = Col["txtNombre"];
                    Cli.NIT = Col["txtNit"];
                    Cli.RazonSocial = Col["txtNombre"];
                    Cli.Telefonos = Col["txtTelefonos"];
                    Cli.Email = Col["txtEmail"];
                    DB.SaveChanges();

                    return Redirect("/Libreria/cliente/");
                    //return View("Index", Pros.ToList());
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
            using (var DB = new ModLibreriaDB())
            {
                Cliente Cli = DB.Cliente.SingleOrDefault(P => P.IdCliente == id);
                DB.Cliente.Remove(Cli);
                DB.SaveChanges();
                var Prods = DB.Cliente.OrderBy(P => P.Nombre).ToList();
                return Redirect("/Libreria/cliente/");
            }
        }
    }
}