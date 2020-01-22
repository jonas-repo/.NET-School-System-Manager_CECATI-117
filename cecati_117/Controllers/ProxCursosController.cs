
using cecati_117.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Controllers
{
    public class ProxCursosController : Controller
    {
        //private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ProxCursos
        public ActionResult Index()
        {
            return View(db.ProximosCursos.ToList().OrderBy(x => x.ProximosCursos_especialidad));
        }
    }
}