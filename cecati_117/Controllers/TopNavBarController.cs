
using cecati_117.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Controllers
{
    public class TopNavBarController : Controller
    {
        // GET: TopNavBar
        // private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();

        public PartialViewResult Index()
        {
            //ViewBag.listaEspecialidades = db.ListaEspecialidades.ToList();
            //ViewBag.listaGalerias = db.Galerias.ToList();
            //foreach(var lista in listaEspecialidades)
            //{
            //    foreach(var pagina in lista.EspecialidadDetalles)
            //    {
            //        pagina.EspecialidadDetalles_ID;
            //    }
            //}

            return PartialView();
        }
    }
}