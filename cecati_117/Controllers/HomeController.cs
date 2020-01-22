using cecati_117.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var noticias_a_mostrar = db.Noticias.ToList().OrderByDescending(x => x.Noticias_Fecha).Take(6);

            return View(noticias_a_mostrar);
        }
      
    }
}