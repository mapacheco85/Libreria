using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Libreria.Models.data;
using Libreria.Seguridad;

namespace Libreria.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        [Authorize]
        public ActionResult Index()
        {
            using (var DB = new LibreriaDB())
            {
                DB.Configuration.LazyLoadingEnabled = false;
                var St = DB.Stock.Include("Producto");
                St.Include("Proveedor");

                return View(St.ToList());
            }
        }

        [Authorize]
        public ActionResult Agregar()
        {
            using (var DB = new LibreriaDB())
            {
                List<SelectListItem> Proveedores = new List<SelectListItem>();
                Proveedores.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "Seleccione Proveedor",
                    Selected = true
                });
                foreach (var Item in DB.Proveedor.OrderBy(P => P.Nombre).ToList())
                {
                    Proveedores.Add(new SelectListItem()
                    {
                        Value = Item.IdProveedor.ToString(),
                        Text = Item.Nombre,
                        Selected = false
                    });
                }

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
                ViewData["Proveedores"] = Proveedores;
                ViewData["Productos"] = Productos;

                return PartialView();
            }
        }

        [Authorize]
        public ActionResult Editar(short id)
        {
            using (var DB = new LibreriaDB())
            {
                var St = DB.Stock.SingleOrDefault(P => P.IdStock == id);

                List<SelectListItem> Proveedores = new List<SelectListItem>();
                foreach (var Item in DB.Proveedor.OrderBy(P => P.Nombre).ToList())
                {
                    Proveedores.Add(new SelectListItem()
                    {
                        Value = Item.IdProveedor.ToString(),
                        Text = Item.Nombre,
                        Selected = Item.IdProveedor == St.IdProveedor ? true : false
                    });
                }
                List<SelectListItem> Productos = new List<SelectListItem>();
                foreach (var Item in DB.Producto.OrderBy(P => P.Nombre).ToList())
                {
                    Productos.Add(new SelectListItem()
                    {
                        Value = Item.IdProducto.ToString(),
                        Text = Item.Nombre,
                        Selected = Item.IdProducto == St.IdProducto ? true : false
                    });
                }
                ViewData["Proveedores"] = Proveedores;
                ViewData["Productos"] = Productos;

                return PartialView(St);
            }
        }

        //POST: Stock
        [HttpPost]
        [Authorize]
        public ActionResult Agregar(FormCollection Col)
        {
            if (Col.Count > 0)
            {
                using (var DB = new LibreriaDB())
                {
                    var Us = DB.Usuario.SingleOrDefault(P => P.Login == User.Identity.Name);

                    Stock St = new Stock();
                    St.IdSucursal = 1;//se debe revisar para el tema de las sucursales.
                    St.IdProveedor = short.Parse(Col["IdProveedor"]);
                    St.IdProducto = short.Parse(Col["IdProducto"]);
                    St.Cantidad = short.Parse(Col["txtCantidad"]);
                    St.PrecioUnitario = decimal.Parse(Col["txtPrecio"]);
                    St.CostoVenta = decimal.Parse(Col["txtCosto"]);
                    try
                    {
                        if (Col["txtVencimiento"] != null)
                            St.FechaVencimiento = DateTime.Parse(Col["txtVencimiento"]);
                        else
                            St.FechaVencimiento = DateTime.Parse("31/12/2030");
                    }
                    catch (Exception)
                    {
                        St.FechaVencimiento = DateTime.Parse("31/12/2030");
                    }
                   
                    St.IdUsuario = short.Parse(Us.IdSujeto.ToString());
                    St.FechaREG = DateTime.Now;
                    St.Activo = true;

                    DB.Stock.Add(St);
                    DB.SaveChanges();

                    //Muestra de nuevo la vista.
                    DB.Configuration.LazyLoadingEnabled = false;

                    var Sts = DB.Stock.Include("Producto");
                    Sts.Include("Proveedor");

                    return Redirect("/Libreria/stock/");
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
                using (var DB = new LibreriaDB())
                {
                    var St = DB.Stock.SingleOrDefault(P => P.IdStock == id);

                    St.IdProveedor = short.Parse(Col["IdProveedor"]);
                    St.IdProducto = short.Parse(Col["IdProducto"]);
                    St.Cantidad = short.Parse(Col["txtCantidad"]);
                    St.PrecioUnitario = decimal.Parse(Col["txtPrecio"]);
                    St.CostoVenta = decimal.Parse(Col["txtCosto"]);
                    if (Col["txtVencimiento"] != null)
                        St.FechaVencimiento = DateTime.Parse(Col["txtVencimiento"]);
                    else
                        St.FechaVencimiento = DateTime.Parse("31/12/2030");

                    DB.SaveChanges();

                    //Muestra de nuevo la vista.
                    DB.Configuration.LazyLoadingEnabled = false;
                    var Sts = DB.Stock.Include("Producto");
                    Sts.Include("Proveedor");

                    return Redirect("/Libreria/stock/");
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
                Stock obj = DB.Stock.SingleOrDefault(U => U.IdStock == id);
                DB.Stock.Remove(obj);
                DB.SaveChanges();

                return Redirect("/Libreria/stock/");
            }
        }

    }
}