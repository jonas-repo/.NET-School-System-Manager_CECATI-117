using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cecati_117.Areas.Admin.ClasesCompartidas;
using cecati_117.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace cecati_117.Areas.Users.Controllers
{
    [Authorize(Roles = "Maestro, Alumno")]
    public class MiPerfilController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private FuncionesUtilesController funcionesUtiles = new FuncionesUtilesController();


        // GET: Users/MiPerfil/Edit/5
        public ActionResult Edit(int? comp)
        {
            if (comp.Equals(1))
            {
                ViewBag.NoEsImagen = "Error por favor sube un archivo con extension de imagen .jpg,.png";
            }
            if (comp.Equals(2))
            {
                ViewBag.Success = "Cambios guardados correctamente";
            }
            ApplicationUser usuarioLogeado = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var persona = db.Persona.Where(x => x.Id_usuario == usuarioLogeado.Id).FirstOrDefault();
            return View(persona);
        }

        // POST: Users/MiPerfil/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Persona_id,Nombre,Apellido_paterno,Apellido_materno,Foto_perfil,Telefono,Email,Genero,Id_usuario,Rol,Numero_control")] Persona persona, HttpPostedFileBase imagen)
        {
   
            if (imagen != null)
            {
                if (funcionesUtiles.Comprobar_SiEsImagen(imagen))
                {
                    if(String.IsNullOrEmpty(persona.Foto_perfil))
                    {
                        persona.Foto_perfil = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/fotos_perfil/", this.Server);
                    }
                    else
                    {
                        funcionesUtiles.QuitarImagen_Servidor(persona.Foto_perfil, this.Server);
                        persona.Foto_perfil = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/fotos_perfil/", this.Server);
                    }                   
                }
                else
                {
                    return RedirectToAction("Edit", new { comp = 1 });
                }
            }           
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { comp = 2 });
            }
            return View(persona);
        }
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
