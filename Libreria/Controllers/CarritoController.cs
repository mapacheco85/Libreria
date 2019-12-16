using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Libreria.Models;
using Libreria.Models.data;

namespace Libreria.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        public ActionResult Index()
        {
            using (var DB = new LibreriaDB())
            {
                DB.Configuration.LazyLoadingEnabled = false;
                var Car = DB.Carrito.Include("Producto").Where(P => P.SessionID == Session.SessionID).ToList();

                return View(Car);
            }
        }

        //public ActionResult Catalogo()
        //{
        //    using (var DB = new LibreriaDB())
        //    {
        //        var Pros = DB.PROListarProductos().ToList();

        //        return View(Pros);
        //    }
        //}

        public ActionResult Catalogo(string filtro)
        {
            if (!String.IsNullOrEmpty(filtro))
            {
                using (var DB = new LibreriaDB())
                {
                    var Pros = DB.PROListarProductos().ToList().FindAll(p => p.Nombre.ToLower().Contains(filtro.ToLower()));

                    return View(Pros);
                }
            }
            else
            {
                using (var DB = new LibreriaDB())
                {
                    var Pros = DB.PROListarProductos().ToList();
                    return View(Pros);
                }
            }
        }

        public ActionResult Pedido()
        {
            using (var DB = new LibreriaDB())
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
            using (var DB = new LibreriaDB())
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
            using (var DB = new LibreriaDB())
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
            using (var DB = new LibreriaDB())
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
            using (var DB = new LibreriaDB())
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
                using (var DB = new LibreriaDB())
                {
                    Pedido Ped = new Pedido();
                    int codPedido = (int)DB.Pedido.Select(p => p.IdPedido).DefaultIfEmpty(0).Max();
                    codPedido++;
                    Ped.IdPedido = codPedido;
                    Ped.SessionID = Session.SessionID;
                    Ped.Nombres = Col["txtNombres"];
                    Ped.Direccion = Col["txtDireccion"];
                    Ped.Ciudad = Col["txtCiudad"];
                    Ped.Email = Col["txtEmail"];
                    Ped.Telefonos = Col["txtTelefonos"];
                    Ped.Observaciones = "";
                    DB.Pedido.Add(Ped);
                    DB.SaveChanges();
                    CorreoModel model1 = new CorreoModel();
                    model1.Nombre = Ped.Nombres;
                    model1.Email = Ped.Email;
                    model1.Telefono = Ped.Telefonos;
                    model1.Mensaje = Ped.Ciudad;
                    model1.Asunto = "PEDIDO DE PAGINA WEB";
                    using (MailMessage mm = new MailMessage())
                    {
                        string to = "libreriaelf@gmail.com";
                        string clave = "Dencrx123";
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Fecha :" + DateTime.Now);
                        sb.AppendLine("Nombre: " + model1.Nombre);
                        sb.AppendLine("Correo: " + model1.Email);
                        sb.AppendLine("Telefono: " + model1.Telefono);
                        sb.AppendLine("Mensaje: " + model1.Mensaje);


                        DB.Configuration.LazyLoadingEnabled = false;
                        var Car = DB.Carrito.Include("Producto").Where(P => P.SessionID == Session.SessionID).ToList();

                        foreach (var item in Car)
                        {
                            sb.AppendLine("Producto: " + item.Producto.Nombre + " Cantidad: " + item.Cantidad);
                        }

                        sb.AppendLine("No se olvide de ejecutar el pedido en las 24 horas posteriores a la solicitud.");
                        mm.From = new MailAddress(model1.Email);
                        mm.To.Add(new MailAddress(to));
                        mm.Subject = model1.Asunto;
                        mm.Body = sb.ToString();
                        mm.IsBodyHtml = false;
                        using (SmtpClient smtp = new SmtpClient())
                        {
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential NetworkCred = new NetworkCredential(to, clave);
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);

                            mm.Dispose();
                            smtp.Dispose();
                        }
                        var lista = (from cc in DB.Carrito where cc.Activo == true select cc);
                        foreach (Carrito carrito in lista.ToList())
                        {
                            carrito.SessionID = "0";
                            carrito.Activo = false;
                            DB.SaveChanges();
                        }

                        return View("~/Views/Carrito/Gracias.cshtml");
                    }
                }
            }
            else
            {
                return HttpNotFound();
            }
        }


    }
}