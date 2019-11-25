using Libreria.Models.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        [Authorize]
        public ActionResult Index()
        {
            using (var DB = new ModLibreriaDB())
            {
                DB.Configuration.LazyLoadingEnabled = false;
                var Pro = DB.Producto.Include("Categoria").OrderBy(P => P.Nombre);

                return View(Pro.ToList());
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
                var Pro = DB.Producto.SingleOrDefault(P => P.IdProducto == id);

                //Se llenan los datos para el combo.
                List<SelectListItem> Categorias = new List<SelectListItem>();
                foreach (var Item in DB.Categoria.OrderBy(P => P.Nombre).ToList())
                {
                    Categorias.Add(new SelectListItem()
                    {
                        Value = Item.IdCategoria.ToString(),
                        Text = Item.Nombre,
                        Selected = Item.IdCategoria == Pro.IdCategoria ? true : false
                    });
                }

                ViewData["Categorias"] = Categorias;
                return PartialView(Pro);
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
                    Producto Pro = new Producto();
                    Pro.IdCategoria = short.Parse(Col["IdCategoria"]);
                    Pro.Nombre = Col["txtNombre"];
                    Pro.Descripcion = Col["txtDescripcion"];
                    Pro.Activo = true;
                    DB.Producto.Add(Pro);
                    DB.SaveChanges();

                    var Pros = DB.Producto.Include("Categoria").OrderBy(P => P.Nombre);
                    return Redirect("/Libreria/producto/");
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
                    Producto Pro = DB.Producto.SingleOrDefault(P => P.IdProducto == id);
                    Pro.IdCategoria = short.Parse(Col["IdCategoria"]);
                    Pro.Nombre = Col["txtNombre"];
                    Pro.Descripcion = Col["txtDescripcion"];
                    DB.SaveChanges();

                    var Pros = DB.Producto.Include("Categoria").OrderBy(P => P.Nombre);
                    return Redirect("/Libreria/producto/");
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
                Producto Pro = DB.Producto.SingleOrDefault(P => P.IdProducto == id);
                DB.Producto.Remove(Pro);

                DB.SaveChanges();

                var Prods = DB.Producto.OrderBy(P => P.Nombre).ToList();

                return Redirect("/Libreria/producto/");
            }
        }
    }
}