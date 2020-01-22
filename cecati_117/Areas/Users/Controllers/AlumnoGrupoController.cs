using cecati_117.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Areas.Users.Controllers
{
    [Authorize(Roles = "Alumno")]
    public class AlumnoGrupoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users/Grupo
        public ActionResult Index()
        {
            ApplicationUser usuarioLogeado = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var persona_logeada = db.Persona.Single(x => x.Id_usuario.Equals(usuarioLogeado.Id));

           

            return View(persona_logeada.Grupos.ToList());
        }
    }
}