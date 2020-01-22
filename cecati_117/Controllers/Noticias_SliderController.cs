
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cecati_117.Models;
using System.Threading;
using System.Globalization;

namespace cecati_117.Controllers
{
    public class Noticias_SliderController : Controller
    {
        // GET: Noticias_Slider
        // private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Noticias_Pagina(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
            {
                return HttpNotFound();
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            ViewBag.Fecha = noticias.Noticias_Fecha.ToLongDateString();

            return View(noticias);
        }

        public PartialViewResult Noticias_recientes()
        {
            var noticias_list = db.Noticias.OrderByDescending(x => x.Noticias_Fecha);
            return PartialView(noticias_list);
        }

        public ActionResult NoticiasTodas()
        {
            var noticias_list = db.Noticias.OrderByDescending(x => x.Noticias_Fecha);
            return View(noticias_list);
        }
    }
}