using Libreria.Models;
using Libreria.Models.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class FacturaController : Controller
    {
        // GET: Factura
        [Authorize]
        public ActionResult Index()
        {
            using (var DB = new ModLibreriaDB())
            {
                DB.Configuration.LazyLoadingEnabled = false;
                var Car = DB.Carrito.Include("Producto").Where(P => P.SessionID == Session.SessionID & P.Transaccion == true & P.Activo == true).ToList();

                List<SelectListItem> Productos = new List<SelectListItem>();
                Productos.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "Seleccione un producto",
                    Selected = true
                });
                foreach (var Item in DB.PROListarProductos().ToList())
                {
                    Productos.Add(new SelectListItem()
                    {
                        Value = Item.IdProducto.ToString(),
                        Text = Item.Nombre,
                        Selected = false
                    });
                }

                ViewData["Productos"] = Productos;
                ViewData["Totales"] = Car.Sum(P => P.CostoTotal).ToString("#,#0.00");
                return View(Car);
            }
        }

        public JsonResult GetSearchValue(string search)
        {
            var DB = new ModLibreriaDB();
            var allsearch = DB.Producto.Where(p => p.Nombre.Contains(search)).Select(p => new { p.IdProducto, p.Nombre }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetSearchValueCode(string search)
        {
            var DB = new ModLibreriaDB();
            var allsearch = DB.Producto.Where(p => p.Codigo.Contains(search)).Select(p => new { p.IdProducto, p.Codigo }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetProductCode(int id)
        {
            var DB = new ModLibreriaDB();
            var allsearch = DB.Producto.Where(p => p.Codigo.Equals(id)).Select(p => new { p.IdProducto, p.Nombre }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult Facturar()
        {
            using (var DB = new ModLibreriaDB())
            {
                DB.Configuration.LazyLoadingEnabled = false;
                var Car = DB.Carrito.Include("Producto").Where(P => P.SessionID == Session.SessionID & P.Transaccion == true & P.Activo == true).ToList();
                ViewData["Totales"] = Car.Sum(P => P.CostoTotal).ToString("#,#0.00");
                return View(Car);
            }
        }


        // POST: Factura
        [HttpPost]
        [Authorize]
        public ActionResult Agregar(FormCollection Col)
        {
            if (Col.Count > 0)
            {
                using (var DB = new ModLibreriaDB())
                {
                    var Pros = DB.PROListarProductos().SingleOrDefault(P => P.Codigo.Equals(Col["Codigo"]));
                    bool res = DB.Database.SqlQuery<bool>(String.Format(@"select dbo.fnControlStock({0},{1})", Pros.IdProducto, int.Parse(Col["txtCantidad"]))).FirstOrDefault<bool>();

                    if (res == true)
                    {
                        int cant = DB.Carrito.Where(c => c.IdProducto == Pros.IdProducto && c.Transaccion == true && c.Activo == true).Count();
                        if (cant == 0)
                        {
                            Carrito Car = new Carrito();
                            Car.SessionID = Session.SessionID;
                            Car.IdProducto = Pros.IdProducto;
                            Car.Cantidad = short.Parse(Col["txtCantidad"]);
                            decimal precio = DB.Database.SqlQuery<decimal>(String.Format(@"select dbo.fnDescuento({0})", Pros.IdProducto)).FirstOrDefault<decimal>();

                            Car.CostoUnidad = precio; //Pros.CostoVenta;
                            Car.CostoTotal = (Car.Cantidad * Car.CostoUnidad);
                            Car.FechaREG = DateTime.Now;
                            Car.Transaccion = true;
                            Car.Activo = true;
                            DB.Carrito.Add(Car);
                            DB.SaveChanges();
                        }
                        else
                        {
                            Carrito car = DB.Carrito.SingleOrDefault(P => P.IdProducto == Pros.IdProducto & P.Transaccion == true & P.Activo == true);
                            short cantNueva = short.Parse(Col["txtCantidad"]);
                            car.Cantidad = (short)(car.Cantidad + cantNueva);
                            DB.SaveChanges();
                        }

                        //Se Cargan los datos para la vista.
                        DB.Configuration.LazyLoadingEnabled = false;
                        var Cars = DB.Carrito.Include("Producto").Where(P => P.SessionID == Session.SessionID & P.Transaccion == true & P.Activo == true).ToList();

                        List<SelectListItem> Productos = new List<SelectListItem>();
                        Productos.Add(new SelectListItem()
                        {
                            Value = "0",
                            Text = "Seleccione un producto",
                            Selected = true
                        });
                        foreach (var Item in DB.PROListarProductos().ToList())
                        {
                            Productos.Add(new SelectListItem()
                            {
                                Value = Item.IdProducto.ToString(),
                                Text = Item.Nombre,
                                Selected = false
                            });
                        }

                        ViewData["Productos"] = Productos;
                        ViewData["Totales"] = Cars.Sum(P => P.CostoTotal).ToString("#,#0.00");

                        return View("Index", Cars);
                    }
                    else
                    {
                        ViewBag.Error = "No hay el stock requerido!!!";
                        return View("Index");
                    }
                    
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Eliminar(decimal id)
        {
            using (var DB = new ModLibreriaDB())
            {
                var Car = DB.Carrito.SingleOrDefault(P => P.IdCarrito == id);
                DB.Carrito.Remove(Car);
                DB.SaveChanges();
                return Content("OK");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Cancelar()
        {
            using (var DB = new ModLibreriaDB())
            {
                var lista = (from cc in DB.Carrito where cc.Activo == true select cc);
                foreach (Carrito carrito in lista.ToList())
                {
                    carrito.Activo = false;
                    DB.SaveChanges();
                }

                return Content("OK");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Imprimir(FormCollection Col)
        {
            if (Col.Count > 0)
            {
                using (var DB = new ModLibreriaDB())
                {
                    var Usu = DB.Usuario.SingleOrDefault(P => P.Login == User.Identity.Name);
                    var Suc = DB.Sucursal.SingleOrDefault(P => P.IdSucursal == 1);
                    var Nro = DB.PROObtenerNroFactura(Suc.NroAutorizacion).SingleOrDefault();
                    //Se crea la Factura
                    Factura Fac = new Factura();
                    Fac.Numero = int.Parse(Nro.ToString());
                    Fac.IdSucursal = Suc.IdSucursal;
                    Fac.Nombre = Col["txtNombre"];
                    Fac.NIT = Col["txtNIT"];
                    Fac.Fecha = DateTime.Now;
                    Fac.FechaREG = DateTime.Now;
                    Fac.IdUsuario = (short)Usu.IdSujeto;
                    Fac.Activo = true;

                    //Se obtienen los detalles de la Factura.
                    var Cars = DB.Carrito.Where(P => P.SessionID == Session.SessionID & P.Transaccion == true & P.Activo == true).ToList();
                    decimal Totales = 0;
                    foreach (var Item in Cars)
                    {
                        Detalle Det = new Detalle();
                        Det.IdProducto = Item.IdProducto;
                        Det.Cantidad = Item.Cantidad;
                        Det.Monto = Math.Round(Item.CostoTotal, 2, MidpointRounding.AwayFromZero);
                        Totales += Det.Monto;
                        Fac.Detalle.Add(Det);
                        DB.PROActualizarStock(Item.IdProducto, Item.Cantidad);
                        Item.Activo = false;
                    }

                    //Se crea el codigo de control.
                    CCodigoControl Cod = new CCodigoControl();
                    Fac.CodigoControl = Cod.GetCodigoControl(Suc.NroAutorizacion, Fac.Numero.ToString(), Fac.NIT, DateTime.Now.ToString("yyyyMMdd"), Math.Round(Totales, 0, MidpointRounding.AwayFromZero).ToString(), Suc.Llave);
                    DB.Factura.Add(Fac);
                    DB.SaveChanges();

                    //Se pasan los datos a mostrar para la impresion de factura.
                    Factura Fact = DB.Factura.Include("Sucursal").SingleOrDefault(P => P.Numero == (int)Nro & P.CodigoControl == Fac.CodigoControl);
                    var Dets = DB.Detalle.Include("Producto").Where(P => P.IdFactura == Fact.IdFactura).ToList();

                    ViewData["Detalle"] = Dets;
                    Num2Letras Lit = new Num2Letras();
                    ViewData["Literal"] = Lit.Convertir(Math.Round(Totales, 2, MidpointRounding.AwayFromZero).ToString());
                    ViewData["Total"] = Math.Round(Totales, 2, MidpointRounding.AwayFromZero).ToString();
                    ViewData["FecLimite"] = ((DateTime)Fact.Sucursal.LimiteEmision).ToString("dd/MM/yyyy");

                    //return View(Fact);
                    return new Rotativa.ViewAsPdf(Fact);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}