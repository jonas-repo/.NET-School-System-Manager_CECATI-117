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
    public class ProximosCursosController : Controller
    {
        //private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();
        private FuncionesUtilesController funcionesUtiles = new FuncionesUtilesController();

        // GET: Admin/ProximosCursos
        public ActionResult Index()
        {
            return View(db.ProximosCursos.ToList().OrderByDescending(x => x.ProximosCursos_fecha));
        }

        // GET: Admin/ProximosCursos/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProximosCursos proximosCursos = db.ProximosCursos.Find(id);
            if (proximosCursos == null)
            {
                return HttpNotFound();
            }
            return View(proximosCursos);
        }

        // GET: Admin/ProximosCursos/Create
        public ActionResult Create(int? comp)
        {
            ViewBag.especialidades = db.ListaEspecialidades.ToList();
            if (comp.Equals(1))
            {
                ViewBag.NoEsImagen = "Error por favor sube un archivo con extension de imagen .jpg,.png";
            }
            return View();
        }

        // POST: Admin/ProximosCursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProximosCursos_ID")] ProximosCursos proximosCursos, HttpPostedFileBase imagen, string especialidad)
        {
            if (funcionesUtiles.Comprobar_SiEsImagen(imagen))
            {
                proximosCursos.ProximosCursos_ImagenURL = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/proximos_cursos/", this.Server);
                proximosCursos.ProximosCursos_especialidad = especialidad;

                if (ModelState.IsValid)
                {
                    proximosCursos.ProximosCursos_fecha = DateTime.Now;
                    proximosCursos.ProximosCursos_ID = Guid.NewGuid();
                    db.ProximosCursos.Add(proximosCursos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Create", new { comp = 1 });
            }
            return View(proximosCursos);
        }

        // GET: Admin/ProximosCursos/Edit/5
        public ActionResult Edit(Guid? id, int? comp)
        {
            if (comp.Equals(1))
            {
                ViewBag.NoEsImagen = "Error por favor sube un archivo con extension de imagen .jpg,.png";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProximosCursos proximosCursos = db.ProximosCursos.Find(id);
            if (proximosCursos == null)
            {
                return HttpNotFound();
            }
            ViewBag.especialidades = db.ListaEspecialidades.ToList();
            return View(proximosCursos);
        }

        // POST: Admin/ProximosCursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProximosCursos_ID,ProximosCursos_ImagenURL")] ProximosCursos proximosCursos, HttpPostedFileBase imagen, string especialidad)
        {
            if (imagen != null)
            {
                if (funcionesUtiles.Comprobar_SiEsImagen(imagen))
                {
                    funcionesUtiles.QuitarImagen_Servidor(proximosCursos.ProximosCursos_ImagenURL, this.Server);
                    proximosCursos.ProximosCursos_ImagenURL = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/proximos_cursos/", this.Server);
                }
                else
                {
                    return RedirectToAction("Edit", new { id = proximosCursos.ProximosCursos_ID, comp = 1 });
                }
            }
            proximosCursos.ProximosCursos_especialidad = especialidad;          
            if (ModelState.IsValid)
            {                   
               db.Entry(proximosCursos).State = EntityState.Modified;
               db.SaveChanges();
               return RedirectToAction("Index");
            }
         
            return View(proximosCursos);

        }

        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProximosCursos proximosCursos = db.ProximosCursos.Find(id);
            if (proximosCursos == null)
            {
                return HttpNotFound();
            }
            funcionesUtiles.QuitarImagen_Servidor(proximosCursos.ProximosCursos_ImagenURL, this.Server);
            db.ProximosCursos.Remove(proximosCursos);
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
