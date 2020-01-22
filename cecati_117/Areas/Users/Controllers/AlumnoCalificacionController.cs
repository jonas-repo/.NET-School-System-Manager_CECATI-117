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
    public class AlumnoCalificacionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ApplicationUser usuarioLogeado = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var persona_logeada = db.Persona.SingleOrDefault(x => x.Id_usuario.Equals(usuarioLogeado.Id));

            var calificaciones = db.Calificacion.Where(x => x.Alumno.Persona_id.Equals(persona_logeada.Persona_id)).ToList();

            ViewBag.calificaciones = calificaciones.OrderBy(x => x.Subtema.Tema.Tema_nombre).ToList();
            ViewBag.persona = persona_logeada;


            return View();
        }
    }
}