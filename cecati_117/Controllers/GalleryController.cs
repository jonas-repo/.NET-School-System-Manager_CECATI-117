
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using cecati_117.Models;

namespace cecati_117.Controllers
{
    public class GalleryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //private Contexto_BaseDatos db = new Contexto_BaseDatos();
        // GET: Gallery
        public ActionResult Index(Guid id, int? page)
        {
            //********* para saber más sobre filtrado de elementos en paginas web consulta aqui https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application //

            var galeria = db.Galerias.Find(id);
            var listaImagenes = galeria.Fotos_Galeria.OrderByDescending(x => x.Fotos_galeria_fecha);
            ViewBag.galeria = galeria;
            int pageSize = 16;
            int pageNumber = (page ?? 1);
            return View(listaImagenes.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Lista_Galerias()
        {
            var galerias = db.Galerias.ToList();
            return View(galerias);
        }

    }
}