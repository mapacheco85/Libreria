using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libreria.Models.data;
using Libreria.ViewModels;
using System.Web.Routing;

namespace Libreria.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        // Listado de Proveedores.
        [Authorize]
        public ActionResult Index(string filtro="", int pagina = 1)
        {
            var totalLista = 10;

            using (var DB = new LibreriaDB())
            {
                var Pro = DB.Proveedor.
                    OrderBy(P => P.Nombre).
                    Where(P => P.Nombre.Contains(filtro)).
                    Skip((pagina - 1) * totalLista)
                    .Take(totalLista).ToList();
                var totalTuplas = DB.Proveedor.Count();
                var modelo = new IndexViewModel();
                modelo.Proveedores = Pro;
                modelo.PaginaActual = pagina;
                modelo.TotalDeRegistros = totalTuplas;
                modelo.RegistrosPorPagina = totalLista;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["filtro"] = filtro;

                return View(modelo);
                //return View(Pro);
            }
        }

        //Nuevo Proveedor
        [Authorize]
        public ActionResult Agregar()
        {
            Proveedor Neo = new Proveedor();

            return PartialView(Neo);
        }

        [Authorize]
        public ActionResult Editar(short id)
        {
            using (var DB = new LibreriaDB())
            {
                var Pro = DB.Proveedor.
                    SingleOrDefault(P => P.IdProveedor == id);

                return PartialView(Pro);
            }
        }

        //POST: Proveedor
        //Registro nuevo Proveedor
        [Authorize]
        [HttpPost]
        public ActionResult Agregar(FormCollection Col)
        {
            if (Col.Count > 0)
            {
                Proveedor Pro = new Proveedor();
                Pro.Nombre = Col["txtNombre"];
                Pro.NIT = Col["txtNit"];
                Pro.Telefonos = Col["txtTelefonos"];
                Pro.Contacto = Col["txtContacto"];
                Pro.Activo = true;

                using (var DB = new LibreriaDB())
                {
                    DB.Proveedor.Add(Pro);
                    DB.SaveChanges();

                    var Provs = DB.Proveedor.OrderBy(P => P.Nombre).ToList();

                    return Redirect("/Libreria/proveedor/");
                    //return View("Index", Provs);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(short id, FormCollection Col)
        {
            if (Col.Count > 0)
            {
                using (var DB = new LibreriaDB())
                {
                    var Pro = DB.Proveedor.SingleOrDefault(P => P.IdProveedor == id);
                    Pro.Nombre = Col["txtNombre"];
                    Pro.NIT = Col["txtNit"];
                    Pro.Telefonos = Col["txtTelefonos"];
                    Pro.Contacto = Col["txtContacto"];
                    DB.SaveChanges();

                    var Provs = DB.Proveedor.OrderBy(P => P.Nombre).ToList();
                    return Redirect("/Libreria/proveedor/");
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
                Proveedor Pro = DB.Proveedor.SingleOrDefault(P => P.IdProveedor == id);
                DB.Proveedor.Remove(Pro);
                DB.SaveChanges();

                var Provs = DB.Proveedor.OrderBy(P => P.Nombre).ToList();
                return Redirect("/Libreria/producto/");
            }
        }
    }
}