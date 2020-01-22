using cecati_117.Areas.Admin.ClasesCompartidas;

using cecati_117.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Controllers
{
    public class Especialidades_PaginasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private FuncionesUtilesController funcionesUtiles = new FuncionesUtilesController();

        // GET: Especialidades_Paginas
        public ActionResult Index(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecialidadDetalles especialidadDetalles = db.EspecialidadDetalles.Find(id);
            if (especialidadDetalles == null)
            {
                return HttpNotFound();
            }
            ViewBag.Actividades = funcionesUtiles.UnirString_EnArray(especialidadDetalles.EspecialidadDetalles_ListaAprendizaje, ',');
            return View(especialidadDetalles);
        }
    }
}