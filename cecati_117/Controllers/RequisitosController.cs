
using cecati_117.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Controllers
{
    public class RequisitosController : Controller
    {
        // GET: Requisitos
        private ApplicationDbContext db = new ApplicationDbContext();
       // private Contexto_BaseDatos db = new Contexto_BaseDatos();
        public ActionResult Index()
        {
            return View(db.RequisitosInscripcion.ToList());
        }
    }
}