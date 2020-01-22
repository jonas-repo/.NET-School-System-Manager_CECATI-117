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
using Microsoft.AspNet.Identity.EntityFramework;

namespace cecati_117.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NumeroControlController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private FuncionesUtilesController funcionesUtiles = new FuncionesUtilesController();

        // GET: Admin/NumeroControl
        public ActionResult Index()
        {
            return View(db.Persona.ToList());
        }

        public PartialViewResult TablaNumerosControl()
        {
            var personas = db.Persona.Where(x => x.Rol.Equals("Alumno")).ToList();

            return PartialView(personas);
        }
        
        public ActionResult Create()
        {

            ViewBag.Especialidad = db.ListaEspecialidades.ToList();
            return View();
        }

        [HttpPost]
        public JsonResult Crear(string[] alumnos, string id_listaEspecialidades)
        {
            string[] alumnos_noRepeat = alumnos;
            ResponseModel responseModel = new ResponseModel(); 
            if(alumnos != null)
            {
                 alumnos_noRepeat = alumnos.Distinct<String>().ToArray();
            }
            

            if (alumnos != null && !String.IsNullOrEmpty(id_listaEspecialidades))
            {
                var personas = db.Persona.Where(r => r.Rol.Equals("Alumno")).ToList();
                foreach(var id in alumnos_noRepeat)
                {
                    var comp = personas.Where(n => n.Numero_control.Equals(id)).FirstOrDefault(); 
                    if (comp == null && !String.IsNullOrEmpty(id) && !String.IsNullOrWhiteSpace(id))
                    {
                        Persona model = new Persona
                        {
                            Persona_id = Guid.NewGuid(),
                            Rol = "Alumno",
                            Nombre = " ",
                            Apellido_materno = " ",
                            Apellido_paterno = " ",
                            Numero_control = id,
                            Genero = true

                        };
                        PersonaEspecialidad personaEspecialidad = new PersonaEspecialidad
                        {
                            PersonaEspecialidad_id = Guid.NewGuid(),
                            Especialidad = db.ListaEspecialidades.Find(new Guid(id_listaEspecialidades)),
                            Persona = model
                        };
                        db.PersonaEspecialidad.Add(personaEspecialidad);
                        model.Especialidades.Add(personaEspecialidad);
                        db.Persona.Add(model);
                    }
                }
                db.SaveChanges();
                responseModel.SetResponse(true);
                responseModel.function = "CargarTabla()";
                responseModel.message = "Cambios guardados correctamente";
                return Json(responseModel);

            }


           // responseModel.href = "/Admin/NumeroControl/Create/";
            responseModel.SetResponse(true);
            return Json(responseModel);
        }
            
        


        // GET: Admin/NumeroControl/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.Especialidad = db.ListaEspecialidades.ToList();
            return View(persona);
        }

        // POST: Admin/NumeroControl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Persona_id,Numero_control")] Persona persona, Guid[] especialidades_id)
        {
            var person = db.Persona.SingleOrDefault(x => x.Persona_id == persona.Persona_id);
            if (ModelState.IsValid && especialidades_id != null)
            {
               
                List<PersonaEspecialidad> personaEspecialidad_existentes = person.Especialidades.ToList();
                person.Numero_control = persona.Numero_control;

                foreach (var especialidad in personaEspecialidad_existentes)
                {

                    if (!especialidades_id.Any(x => x.Equals(especialidad.PersonaEspecialidad_id)))
                    {
                        person.Especialidades.Remove(especialidad);
                        db.PersonaEspecialidad.Remove(especialidad);
                    }

                }
                foreach (var id_guid in especialidades_id)
                {
                    PersonaEspecialidad personaEspecialidad = new PersonaEspecialidad
                    {
                        PersonaEspecialidad_id = Guid.NewGuid(),
                        Especialidad = db.ListaEspecialidades.Find(id_guid),
                        Persona = person
                    };
                    db.PersonaEspecialidad.Add(personaEspecialidad);
                    person.Especialidades.Add(personaEspecialidad);
                }


                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Success = "Cambios guardados correctamente";
                ViewBag.Especialidad = db.ListaEspecialidades.ToList();
                return View(person);
            }
            ViewBag.Especialidad = db.ListaEspecialidades.ToList();
            return View(person);
        }

        public ActionResult DeleteConfirmed(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }

            funcionesUtiles.QuitarImagen_Servidor(persona.Foto_perfil, this.Server);
            List<PersonaEspecialidad> personaEspecialidad = persona.Especialidades.ToList();
            foreach (var especialidad in personaEspecialidad)
            {
                db.PersonaEspecialidad.Remove(especialidad);
            }
            if(!String.IsNullOrEmpty(persona.Id_usuario))
            {
                ApplicationUser user = db.Users.Find(persona.Id_usuario);
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                userManager.Delete(user);
            }

            db.Persona.Remove(persona);
            db.SaveChanges();
            return RedirectToAction("Create");
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
