using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Libreria.Models;
using Libreria.Models.data;
using System.Text;

namespace Libreria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var DB = new ModLibreriaDB())
            {
                var Promos = DB.PROListarPromociones().ToList();

                ViewData["ID"] = HttpContext.Session.SessionID;

                return View(Promos);
            }

        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult Promos()
        {
            using (var DB = new ModLibreriaDB())
            {
                var Promos = DB.PROListarPromociones().ToList();

                return View(Promos);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CorreoModel model)
        {
            if (ModelState.IsValid)
            {
                using (MailMessage mm = new MailMessage())
                {
                    string to = "miguelangelpachecoarteaga@gmail.com";
                    string clave = "23212";
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Nombre: " + model.Nombre);
                    sb.AppendLine("Correo: " + model.Email);
                    sb.AppendLine("Telefono: " + model.Telefono);
                    sb.AppendLine("Mensaje: " + model.Mensaje);
                    mm.From = new MailAddress(model.Email);
                    mm.To.Add(new MailAddress(to));
                    mm.Subject = model.Asunto;
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
                        return RedirectToAction("Contact");
                    }
                }
            }
            return View();
        }
    }
}