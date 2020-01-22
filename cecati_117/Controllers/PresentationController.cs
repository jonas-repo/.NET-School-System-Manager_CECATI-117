
using cecati_117.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Controllers
{
    public class PresentationController : Controller
    {
        // private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Presentation
        public ActionResult Index()
        {
            return View(db.Presentacion.ToList().OrderBy(x => x.Presentacion_numero));
        }
    }
}