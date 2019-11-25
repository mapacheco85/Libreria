using Libreria.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Libreria.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            var items = CReports.ListadoReportes();
            ViewBag.TipoReportes = new SelectList(items);


            return View();
        }

    }
}