using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using cecati_117.Models;

namespace cecati_117.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RequisitosInscripcionsController : Controller
    {
        // private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/RequisitosInscripcions
        public ActionResult Index()
        {
            return View(db.RequisitosInscripcion.ToList());
        }

        // GET: Admin/RequisitosInscripcions/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitosInscripcion requisitosInscripcion = db.RequisitosInscripcion.Find(id);
            if (requisitosInscripcion == null)
            {
                return HttpNotFound();
            }
            return View(requisitosInscripcion);
        }

        // GET: Admin/RequisitosInscripcions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RequisitosInscripcions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequisitosInscripcion_ID,RequisitosInscripcion_titulo")] RequisitosInscripcion requisitosInscripcion, string __descr, string icono)
        {
            requisitosInscripcion.RequisitosInscripcion_descripcion = __descr;
            requisitosInscripcion.RequisitosInscripcion_icono = icono;
            if (ModelState.IsValid)
            {
                requisitosInscripcion.RequisitosInscripcion_ID = Guid.NewGuid();
                db.RequisitosInscripcion.Add(requisitosInscripcion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requisitosInscripcion);
        }

        // GET: Admin/RequisitosInscripcions/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitosInscripcion requisitosInscripcion = db.RequisitosInscripcion.Find(id);
            if (requisitosInscripcion == null)
            {
                return HttpNotFound();
            }
            return View(requisitosInscripcion);
        }

        // POST: Admin/RequisitosInscripcions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequisitosInscripcion_ID,RequisitosInscripcion_titulo")] RequisitosInscripcion requisitosInscripcion, string __descr, string icono)
        {
            requisitosInscripcion.RequisitosInscripcion_descripcion = __descr;
            requisitosInscripcion.RequisitosInscripcion_icono = icono;
            if (ModelState.IsValid)
            {
                db.Entry(requisitosInscripcion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requisitosInscripcion);
        }

        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitosInscripcion requisitosInscripcion = db.RequisitosInscripcion.Find(id);
            if (requisitosInscripcion == null)
            {
                return HttpNotFound();
            }
            db.RequisitosInscripcion.Remove(requisitosInscripcion);
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
