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
    public class BolsaDeTrabajoController : Controller
    {
       // private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/BolsaDeTrabajo
        public ActionResult Index()
        {
            return View(db.BolsaDeTrabajo.ToList());
        }

        // GET: Admin/BolsaDeTrabajo/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BolsaDeTrabajo bolsaDeTrabajo = db.BolsaDeTrabajo.Find(id);
            if (bolsaDeTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(bolsaDeTrabajo);
        }

        // GET: Admin/BolsaDeTrabajo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BolsaDeTrabajo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BolsaDeTrabajo_ID,BolsaDeTrabajo_Titulo")] BolsaDeTrabajo bolsaDeTrabajo, string __descr)
        {
            bolsaDeTrabajo.BolsaDeTrabajo_Descripción = __descr;
            if (ModelState.IsValid)
            {
                bolsaDeTrabajo.BolsaDeTrabajo_Fecha = DateTime.Now;
                bolsaDeTrabajo.BolsaDeTrabajo_ID = Guid.NewGuid();
                db.BolsaDeTrabajo.Add(bolsaDeTrabajo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bolsaDeTrabajo);
        }

        // GET: Admin/BolsaDeTrabajo/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BolsaDeTrabajo bolsaDeTrabajo = db.BolsaDeTrabajo.Find(id);
            if (bolsaDeTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(bolsaDeTrabajo);
        }

        // POST: Admin/BolsaDeTrabajo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BolsaDeTrabajo_ID,BolsaDeTrabajo_Titulo")] BolsaDeTrabajo bolsaDeTrabajo, string __descr)
        {
            bolsaDeTrabajo.BolsaDeTrabajo_Descripción = __descr;
            if (ModelState.IsValid)
            {
                bolsaDeTrabajo.BolsaDeTrabajo_Fecha = DateTime.Now;
                db.Entry(bolsaDeTrabajo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bolsaDeTrabajo);
        }

        // POST: Admin/BolsaDeTrabajo/Delete/5
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BolsaDeTrabajo bolsaDeTrabajo = db.BolsaDeTrabajo.Find(id);
            if (bolsaDeTrabajo == null)
            {
                return HttpNotFound();
            }
            db.BolsaDeTrabajo.Remove(bolsaDeTrabajo);
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
