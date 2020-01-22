using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using cecati_117.Models;

namespace cecati_117.Controllers
{
    public class ServicesController : Controller
    {
        //private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Services
        public ActionResult Index()
        {            
            return View(db.Servicios.ToList());
        }
    }
}