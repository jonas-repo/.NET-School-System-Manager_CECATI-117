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
    public class PresentacionController : Controller
    {
        // private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Presentacion
        public ActionResult Index()
        {
            return View(db.Presentacion.ToList().OrderBy(x => x.Presentacion_numero));
        }

        // GET: Admin/Presentacion/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presentacion presentacion = db.Presentacion.Find(id);
            if (presentacion == null)
            {
                return HttpNotFound();
            }
            return View(presentacion);
        }

        // GET: Admin/Presentacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Presentacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Presentacion_ID,Presentacion_titulo,Presentacion_numero")] Presentacion presentacion, string __descr)
        {
            presentacion.Presentacion_descripcion = __descr;
            if (ModelState.IsValid)
            {
                presentacion.Presentacion_ID = Guid.NewGuid();
                db.Presentacion.Add(presentacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(presentacion);
        }

        // GET: Admin/Presentacion/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presentacion presentacion = db.Presentacion.Find(id);
            if (presentacion == null)
            {
                return HttpNotFound();
            }
            return View(presentacion);
        }

        // POST: Admin/Presentacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Presentacion_ID,Presentacion_titulo,Presentacion_numero")] Presentacion presentacion, string __descr)
        {
            presentacion.Presentacion_descripcion = __descr;
            if (ModelState.IsValid)
            {
                db.Entry(presentacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(presentacion);
        }

        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presentacion presentacion = db.Presentacion.Find(id);
            if (presentacion == null)
            {
                return HttpNotFound();
            }
            db.Presentacion.Remove(presentacion);
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
