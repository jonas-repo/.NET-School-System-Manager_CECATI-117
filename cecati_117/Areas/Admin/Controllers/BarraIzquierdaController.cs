
using cecati_117.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BarraIzquierdaController : Controller
    {
        // GET: Admin/BarraIzquierda
        private ApplicationDbContext db = new ApplicationDbContext();
       // private Contexto_BaseDatos db = new Contexto_BaseDatos();

        public PartialViewResult Izquierda()
        {
            ApplicationUser usuarioLogeado = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ViewBag.NombreUsuario = usuarioLogeado.FullName;


            return PartialView();
        }

    }
}