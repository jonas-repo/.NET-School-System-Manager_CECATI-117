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
   [Authorize(Roles = "Maestro, Alumno")]
    public class BarraIzquierda_UsersController : Controller
    {
        // GET: Users/BarraIzquierda_Users
        // GET: Admin/BarraIzquierda
        private ApplicationDbContext db = new ApplicationDbContext();
        // private Contexto_BaseDatos db = new Contexto_BaseDatos();

        public PartialViewResult Izquierda()
        {
            ApplicationUser usuarioLogeado = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var usuario = db.Persona.Where(x => x.Id_usuario == usuarioLogeado.Id).FirstOrDefault();
            if(usuario == null)
            {
                ViewBag.Usuario = new Persona()
                {
                    Nombre = "",
                    Foto_perfil = "",
                    Genero = true
                    
                };
            }
            else
            {
                ViewBag.Usuario = usuario;
            }

            return PartialView();
        }
    }
}