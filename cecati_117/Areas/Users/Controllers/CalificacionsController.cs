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
    public class CalificacionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users/Calificacions
        public ActionResult Index()
        {
            ApplicationUser usuarioLogeado = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var persona_logeada = db.Persona.Single(x => x.Id_usuario.Equals(usuarioLogeado.Id));

            return View(persona_logeada.Grupos.ToList());
        }

        public ActionResult TemasIndex(Guid? id_grupo)
        {
            if(id_grupo != null)
            {
                var grupo = db.Grupo.Find(id_grupo);
                if(grupo == null)
                {
                    return RedirectToAction("Index");
                }
                var especialidad = db.ListaEspecialidades.Find(new Guid(grupo.Especialidad));
                ViewBag.Temas = especialidad.Temas.ToList();
                ViewBag.grupo = grupo;
                ViewBag.especialidad = especialidad;



                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult SubtemasCalificacion(Guid? id_subtema, Guid? id_grupo)
        {
            if(id_subtema != null && id_grupo != null)
            {
                var grupo = db.Grupo.Find(id_grupo);
                var subtema = db.Subtema.Find(id_subtema);
                if (grupo == null || subtema == null)
                {
                    return RedirectToAction("Index");
                }

                List<Calificados> calificados = new List<Calificados>();
                List<Calificados> noCalificados = new List<Calificados>();

                foreach(var persona in grupo.Personas.Where(r => r.Rol.Equals("Alumno")))
                {
                    var calificacion = db.Calificacion.SingleOrDefault(
                        x => x.Alumno.Persona_id.Equals(persona.Persona_id)
                        && x.Subtema.Subtema_id.Equals(subtema.Subtema_id));

                    if(calificacion == null)
                    {
                        Calificados noCalificado = new Calificados()
                        {
                            Persona = persona,
                            Subtema = subtema
                        };
                        noCalificados.Add(noCalificado);
                    }
                    else
                    {
                        Calificados Calificado = new Calificados()
                        {
                            Persona = persona,
                            Subtema = subtema,
                            Calificacion = calificacion
                        };
                        calificados.Add(Calificado);
                    }

                }


                ViewBag.calificados = calificados;
                ViewBag.NoCalificados = noCalificados;
                ViewBag.grupo = grupo;
                ViewBag.subtema = subtema;


                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CalificacionRegister(List<Calificacion> noCalificados, List<Calificacion> calificados, Guid? grupo, Guid? subtema)
        {
            if (calificados != null)
            {
                foreach (var calification in calificados)
                {
                    var calificacion_existente = db.Calificacion.Find(calification.Calificacion_id);
                    if (calificacion_existente == null)
                    {
                        return RedirectToAction("Index");
                    }
                    calificacion_existente.Calificacion_total = calification.Calificacion_total;
                    db.Entry(calificacion_existente).State = EntityState.Modified;
                }
            }
            if (noCalificados != null)
            {
                foreach (var calificacion in noCalificados)
                {
                    calificacion.Alumno = db.Persona.Find(calificacion.Alumno.Persona_id);
                    calificacion.Subtema = db.Subtema.Find(calificacion.Subtema.Subtema_id);
                    if (calificacion.Alumno == null || calificacion.Subtema == null)
                    {
                        return RedirectToAction("Index");
                    }
                    calificacion.Calificacion_id = Guid.NewGuid();
                    db.Calificacion.Add(calificacion);
                }
            }
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            return RedirectToAction("SubtemasCalificacion", new { id_subtema = subtema, id_grupo = grupo });
        }




        // GET: Users/Calificacions/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // GET: Users/Calificacions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Calificacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Calificacion_id,Calificacion_total")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                calificacion.Calificacion_id = Guid.NewGuid();
                db.Calificacion.Add(calificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(calificacion);
        }

        // GET: Users/Calificacions/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // POST: Users/Calificacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Calificacion_id,Calificacion_total")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calificacion);
        }

        // GET: Users/Calificacions/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // POST: Users/Calificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Calificacion calificacion = db.Calificacion.Find(id);
            db.Calificacion.Remove(calificacion);
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
