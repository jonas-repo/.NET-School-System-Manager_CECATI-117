using cecati_117.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Areas.Users.Controllers
{
    [Authorize(Roles = "Maestro, Alumno")]
    public class PaginaInicioUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users/PaginaInicio_Users
        public ActionResult Index()
        {
            var noticias = db.Noticias.ToList().OrderByDescending(x => x.Noticias_Fecha).Take(9);
            ViewBag.bolsaTrabajo = db.BolsaDeTrabajo.ToList().OrderByDescending(x => x.BolsaDeTrabajo_Fecha).Take(15);
            ViewBag.proximosCursos = db.ProximosCursos.ToList().OrderByDescending(x => x.ProximosCursos_fecha).Take(15);
            return View(noticias);
        }
    }
}