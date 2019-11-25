using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libreria.Models.data;

namespace Libreria.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        public ActionResult Index()
        {
            using (var DB = new ModLibreriaDB())
            {
                DB.Configuration.LazyLoadingEnabled = false;
                var Car = DB.Carrito.Include("Producto").Where(P => P.SessionID == Session.SessionID).ToList();

                return View(Car);
            }
        }

        public ActionResult Catalogo(string filtro)
        {
            if (!String.IsNullOrEmpty(filtro))
            {
                using (var DB = new ModLibreriaDB())
                {
                    var Pros = DB.PROListarProductos().ToList().FindAll(p => p.Nombre.ToLower().Contains(filtro.ToLower()));

                    return View(Pros);
                }
            }
            else
            {
                using (var DB = new ModLibreriaDB())
                {
                    var Pros = DB.PROListarProductos().ToList();
                    return View(Pros);
                }
            }
        }

        public ActionResult Pedido()
        {
            using (var DB = new ModLibreriaDB())
            {
                DB.Configuration.LazyLoadingEnabled = false;
                var Car = DB.Carrito.Include("Producto").Where(P => P.SessionID == Session.SessionID).ToList();

                var Totales = Car.Sum(P => P.CostoTotal).ToString("#,#0.00");
                ViewData["Totales"] = Totales;

                return View(Car);
            }
        }


        //POST: Carrito

        [HttpPost]
        public ActionResult Agregar(short id)
        {
            using (var DB = new ModLibreriaDB())
            {
                var Pro = DB.PROObtenerProductoStock(id).SingleOrDefault();
                Carrito Car = new Carrito();
                Car.SessionID = Session.SessionID;
                Car.IdProducto = Pro.IdProducto;
                Car.Cantidad = 1;
                Car.CostoUnidad = (decimal)Pro.Cobrar;
                Car.CostoTotal = Car.Cantidad * Car.CostoUnidad;
                Car.FechaREG = DateTime.Now;
                Car.Transaccion = false;
                Car.Activo = true;

                DB.Carrito.Add(Car);

                DB.SaveChanges();


                DB.Configuration.LazyLoadingEnabled = false;

                var Cars = DB.Carrito.Include("Producto").Where(P => P.SessionID == Session.SessionID).ToList();

                return View("Index", Cars);
            }
        }

        [HttpPost]
        //[Route("editar/{id}/{cantidad}")]
        public ActionResult Editar(decimal id, short cantidad)
        {
            using (var DB = new ModLibreriaDB())
            {
                var Car = DB.Carrito.SingleOrDefault(P => P.IdCarrito == id);
                Car.Cantidad = cantidad;
                Car.CostoTotal = Car.Cantidad * Car.CostoUnidad;

                DB.SaveChanges();

                return Content("OK");
            }
        }

        [HttpPost]
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
        public ActionResult CancelarPedido()
        {
            using (var DB = new ModLibreriaDB())
            {
                var lista = (from cc in DB.Carrito where cc.Activo == true select cc);
                foreach (Carrito carrito in lista.ToList())
                {
                    carrito.SessionID = "0";
                    carrito.Activo = false;
                    DB.SaveChanges();
                }

                return Content("OK");
            }
        }

        [HttpPost]
        public ActionResult Pedido(FormCollection Col)
        {
            if (Col.Count > 0)
            {
                using (var DB = new ModLibreriaDB())
                {
                    Pedido Ped = new Pedido();
                    Ped.SessionID = Session.SessionID;
                    Ped.Nombres = Col["txtNombres"];
                    Ped.Direccion = Col["txtDireccion"];
                    Ped.Ciudad = Col["txtCiudad"];
                    Ped.Email = Col["txtEmail"];
                    Ped.Telefonos = Col["txtTelefonos"];
                    Ped.Observaciones = "";

                    DB.Pedido.Add(Ped);

                    DB.SaveChanges();


                    return View("Gracias", "Carrito");
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

    }
}