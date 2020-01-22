using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cecati_117.Models;

namespace cecati_117.Controllers
{
    public class PortFolioSlider_especialidadesController : Controller
    {
        // private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: PortFolioSlider_especialidades
        public PartialViewResult PortFolio_Slider()
        {
            var list = db.EspecialidadDetalles.OrderBy(x => Guid.NewGuid()).ToList();
            return PartialView(list);
        }
    }
}