using Libreria.Models.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class PromoController : Controller
    {
        // GET: Promo
        [Authorize]
        public ActionResult Index()
        {
            using (var DB = new ModLibreriaDB())
            {
                DB.Configuration.LazyLoadingEnabled = false;
                var Des = DB.Descuento.Include("Producto").OrderByDescending(P => P.FechaFin).ToList();
                return View(Des);
            }
        }

        [Authorize]
        public ActionResult Agregar()
        {
            using (var DB = new ModLibreriaDB())
            {
                List<SelectListItem> Productos = new List<SelectListItem>();
                Productos.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "Seleccione Producto",
                    Selected = true
                });

                foreach (var Item in DB.Producto.OrderBy(P => P.Nombre).ToList())
                {
                    Productos.Add(new SelectListItem()
                    {
                        Value = Item.IdProducto.ToString(),
                        Text = Item.Nombre,
                        Selected = false
                    });
                }

                ViewData["Productos"] = Productos;

                return PartialView();
            }
        }

        [Authorize]
        public ActionResult Editar(short id)
        {
            using (var DB = new ModLibreriaDB())
            {
                var Des = DB.Descuento.SingleOrDefault(P => P.IdDescuento == id);
                List<SelectListItem> Productos = new List<SelectListItem>();
                foreach (var Item in DB.Producto.OrderBy(P => P.Nombre).ToList())
                {
                    Productos.Add(new SelectListItem()
                    {
                        Value = Item.IdProducto.ToString(),
                        Text = Item.Nombre,
                        Selected = Item.IdProducto == Des.IdProducto ? true : false
                    });
                }
                ViewData["Productos"] = Productos;
                return PartialView(Des);
            }
        }

        // POST: Promo
        [HttpPost]
        [Authorize]
        public ActionResult Agregar(FormCollection Col)
        {
            if (Col.Count > 0)
            {
                using (var DB = new ModLibreriaDB())
                {
                    Descuento Des = new Descuento();
                    Des.IdProducto = short.Parse(Col["IdProducto"]);
                    Des.Porcentaje = decimal.Parse(Col["txtPorcentaje"]) / 10;
                    Des.FechaIni = DateTime.Parse(Col["txtIni"]);
                    Des.FechaFin = DateTime.Parse(Col["txtFin"]);
                    Des.Activo = true;
                    DB.Descuento.Add(Des);
                    DB.SaveChanges();

                    DB.Configuration.LazyLoadingEnabled = false;
                    var Dess = DB.Descuento.Include("Producto").OrderByDescending(P => P.FechaFin).ToList();

                    return Redirect("/Libreria/promo/");
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
                    Descuento Des = DB.Descuento.SingleOrDefault(P => P.IdDescuento == id);
                    Des.IdProducto = short.Parse(Col["IdProducto"]);
                    Des.Porcentaje = decimal.Parse(Col["txtPorcentaje"]) / 10;
                    Des.FechaIni = DateTime.Parse(Col["txtIni"]);
                    Des.FechaFin = DateTime.Parse(Col["txtFin"]);
                    DB.SaveChanges();

                    DB.Configuration.LazyLoadingEnabled = false;
                    var Dess = DB.Descuento.Include("Producto").OrderByDescending(P => P.FechaFin).ToList();

                    return Redirect("/Libreria/promo/");
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
                Descuento obj = DB.Descuento.SingleOrDefault(U => U.IdDescuento == id);
                DB.Descuento.Remove(obj);
                DB.SaveChanges();

                return Redirect("/Libreria/promo/");
            }
        }
    }
}