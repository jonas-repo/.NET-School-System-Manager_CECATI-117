using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cecati_117.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace cecati_117.Areas.Users.Controllers
{
    [Authorize(Roles = "Maestro")]
    public class GrupoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users/Grupo
        public ActionResult Index()
        {
            ApplicationUser usuarioLogeado = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var persona_logeada = db.Persona.Single(x => x.Id_usuario.Equals(usuarioLogeado.Id));

            return View(persona_logeada.Grupos.ToList());
        }

        public ActionResult AddAlumns(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupo.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            var personas_conEspecialidad = db.PersonaEspecialidad.Where(
                x => x.Especialidad.ListaEspecialidades_ID.Equals(new Guid(grupo.Especialidad))).ToList();

            List<Persona> personas = new List<Persona>();
            foreach(var persona in personas_conEspecialidad)
            {
                var coincide = persona.Persona.Grupos.SingleOrDefault(
                                x => x.Grupo_id.Equals(grupo.Grupo_id));
                if(coincide == null)
                {
                    personas.Add(persona.Persona);
                }
            }
            ViewBag.ListaNoEnGrupo = personas;
            ViewBag.Grupo = grupo;
            return View();
            
        }

        [HttpPost]
        public ActionResult AddSelected(Guid[] personasToGroup, Guid grupo)
        {          
            if(grupo == null)
            {
                return RedirectToAction("Index");
            }
            var grupo_registrar = db.Grupo.Find(grupo);
            if (personasToGroup != null)
            {
                foreach(var id in personasToGroup)
                {
                    var persona = db.Persona.Find(id);
                    persona.Grupos.Add(grupo_registrar);
                    db.Entry(persona).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("AddAlumns", new { id = grupo_registrar.Grupo_id });
            }
            else
            {
                return RedirectToAction("AddAlumns", new { id = grupo_registrar.Grupo_id });
            }
        }

        public ActionResult DeleteSelected( Guid Persona, Guid grupo )
        {
            if(Persona == null || grupo == null)
            {
                return RedirectToAction("Index");
            }
            var grupo_quitar = db.Grupo.Find(grupo);
            var persona_toRemoveGroup = db.Persona.Find(Persona);
            persona_toRemoveGroup.Grupos.Remove(grupo_quitar);
            db.Entry(persona_toRemoveGroup).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("AddAlumns", new { id = grupo_quitar.Grupo_id });
        }



        // GET: Users/Grupo/Create
        public ActionResult Create()
        {
            ApplicationUser usuarioLogeado = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var persona_logeada = db.Persona.Single(x => x.Id_usuario.Equals(usuarioLogeado.Id));
            ViewBag.persona = persona_logeada;
            return View();
        }

        // POST: Users/Grupo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Grupo_id,Nombre_grupo,Turno,Ciclo_escolar,Horario")] Grupo grupo, string id_listaEspecialidades)
        {
            ApplicationUser usuarioLogeado = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var persona_logeada = db.Persona.Single(x => x.Id_usuario.Equals(usuarioLogeado.Id));
            if (ModelState.IsValid)
            {
                
                grupo.Grupo_id = Guid.NewGuid();
                grupo.Especialidad = id_listaEspecialidades;
                db.Grupo.Add(grupo);
                persona_logeada.Grupos.Add(grupo);
                db.Entry(persona_logeada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.persona = persona_logeada;
            return View(grupo);
        }

        // GET: Users/Grupo/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupo.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            ApplicationUser usuarioLogeado = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var persona_logeada = db.Persona.Single(x => x.Id_usuario.Equals(usuarioLogeado.Id));
            ViewBag.persona = persona_logeada;
            return View(grupo);
        }

        // POST: Users/Grupo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Grupo_id,Nombre_grupo,Turno,Ciclo_escolar,Horario")] Grupo grupo , string id_listaEspecialidades)
        {
            if (ModelState.IsValid)
            {
                grupo.Especialidad = id_listaEspecialidades;
                db.Entry(grupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupo);
        }

        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupo.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            db.Grupo.Remove(grupo);
            db.SaveChanges();
            return RedirectToAction("Index");
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
