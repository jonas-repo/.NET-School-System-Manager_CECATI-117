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

namespace cecati_117.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ListaEspecialidadesController : Controller
    {
        //private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationDbContext db2 = new ApplicationDbContext();
        private FuncionesUtilesController funcionesUtiles = new FuncionesUtilesController();
        // GET: Admin/ListaEspecialidades
        public ActionResult Index()
        {

            return View(db.ListaEspecialidades.ToList());
        }

        // GET: Admin/ListaEspecialidades/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaEspecialidades listaEspecialidades = db.ListaEspecialidades.Find(id);
            if (listaEspecialidades == null)
            {
                return HttpNotFound();
            }
            return View(listaEspecialidades);
        }

        // GET: Admin/ListaEspecialidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ListaEspecialidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ListaEspecialidades_ID,ListaEspecialidades_especialidad")] ListaEspecialidades listaEspecialidades, string[] temas)
        {
            if (ModelState.IsValid)
            {
                listaEspecialidades.ListaEspecialidades_ID = Guid.NewGuid();
                listaEspecialidades.Temas = new List<Tema>();
                db.ListaEspecialidades.Add(listaEspecialidades);
                foreach (var tema in temas)
                {
                    Tema tema_nuevo = new Tema()
                    {
                        Tema_id = Guid.NewGuid(),
                        Tema_nombre = tema,
                        Especialidad = listaEspecialidades
                    };
                    listaEspecialidades.Temas.Add(tema_nuevo);
                    db.Tema.Add(tema_nuevo);
                }                                         
                db.SaveChanges();             
                return RedirectToAction("Index");
            }

            return View(listaEspecialidades);
        }

        // GET: Admin/ListaEspecialidades/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaEspecialidades listaEspecialidades = db.ListaEspecialidades.Find(id);
            if (listaEspecialidades == null)
            {
                return HttpNotFound();
            }
            return View(listaEspecialidades);
        }

        // POST: Admin/ListaEspecialidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ListaEspecialidades_ID,ListaEspecialidades_especialidad")] ListaEspecialidades listaEspecialidades, string[] temas_nuevos, List<Tema> temas_editados)
        {
            var especialidad_existente = db.ListaEspecialidades.Find(listaEspecialidades.ListaEspecialidades_ID);
            if(especialidad_existente == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                listaEspecialidades.Temas = new List<Tema>();
                if(temas_editados == null)
                {
                    List<Tema> temas_toDelete = new List<Tema>();
                    temas_toDelete = especialidad_existente.Temas.ToList();
                    foreach (var tema in temas_toDelete)
                    {
                        db.Tema.Remove(tema);
                    }

                }
                if(temas_editados != null )
                {
                    List<Tema> temas_a_borrar = new List<Tema>();
                    temas_a_borrar = especialidad_existente.Temas.ToList();

                    foreach (var tema_existente in temas_a_borrar)
                    {
                        Tema tema_editado = temas_editados.Where(x => x.Tema_id.Equals(tema_existente.Tema_id)).FirstOrDefault();
                        if( tema_editado != null)
                        {
                            tema_existente.Tema_nombre = tema_editado.Tema_nombre;
                            db.Entry(tema_existente).State = EntityState.Modified;
                            especialidad_existente.Temas.Add(tema_existente);
                            db.SaveChanges();
                        }
                        else
                        {

                            db.Tema.Remove(tema_existente);
                        }
                        
                    }
                }
                if (!(temas_nuevos == null || temas_nuevos.Length == 0))
                {
                    foreach (var tema in temas_nuevos)
                    {
                        Tema tema_nuevo = new Tema()
                        {
                            Tema_id = Guid.NewGuid(),
                            Tema_nombre = tema,
                            Especialidad = especialidad_existente
                        };
                        especialidad_existente.Temas.Add(tema_nuevo);
                        db.Tema.Add(tema_nuevo);
                    }
                }



                especialidad_existente.ListaEspecialidades_especialidad = listaEspecialidades.ListaEspecialidades_especialidad;
                db.Entry(especialidad_existente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(especialidad_existente);
        }

        // GET: Admin/ListaEspecialidades/Delete/5
        // POST: Admin/ListaEspecialidades/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ListaEspecialidades listaEspecialidades = db.ListaEspecialidades.Find(id);
            if (listaEspecialidades == null)
            {
                return HttpNotFound();
            }


            var lista = listaEspecialidades.EspecialidadDetalles.ToList();
            foreach(var pagina in lista)
            {
                funcionesUtiles.QuitarImagen_Servidor(pagina.EspecialidadDetalles_Imagen, this.Server);
                db.EspecialidadDetalles.Remove(pagina);
            }


            var temas = listaEspecialidades.Temas.ToList();                     
            foreach(var tema in temas)
            {
                var tema_sub = tema.Subtemas.ToList();
                foreach(var subtema in tema_sub)
                {
                    

                    db.Subtema.Remove(subtema);
                }
                db.Tema.Remove(tema);
            }


            db.ListaEspecialidades.Remove(listaEspecialidades);
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
