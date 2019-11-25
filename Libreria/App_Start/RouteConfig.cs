using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Libreria
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //  name: "CarritoCompra",
            //  url: "Editar/{id}/{cantidad}",
            //  defaults: new {controller = "Carrito", action="Editar", id=UrlParameter.Optional,cantidad=UrlParameter.Optional}
            //  );
        }
    }
}
