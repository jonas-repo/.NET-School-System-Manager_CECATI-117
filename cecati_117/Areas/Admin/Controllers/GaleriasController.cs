using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cecati_117.Areas.Admin.ClasesCompartidas;

using cecati_117.Models;

namespace cecati_117.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GaleriasController : Controller
    {
        //private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();
        private FuncionesUtilesController funcionesUtiles = new FuncionesUtilesController();

        // GET: Admin/Galerias
        public ActionResult Index()
        {
            return View(db.Galerias.ToList());
        }

        // GET: Admin/Galerias/Create
        public ActionResult Create()
        {
            ViewBag.especialidades = db.ListaEspecialidades.ToList();
            return View();
        }

        // POST: Admin/Galerias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Galeria_ID,Galeria_titulo")] Galeria galeria)
        {
            if (ModelState.IsValid)
            {
                galeria.Galeria_ID = Guid.NewGuid();
                db.Galerias.Add(galeria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(galeria);
        }

        // GET: Admin/Galerias/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Galeria galeria = db.Galerias.Find(id);
            if (galeria == null)
            {
                return HttpNotFound();
            }
            return View(galeria);
        }

        // POST: Admin/Galerias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Galeria_ID,Galeria_titulo")] Galeria galeria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(galeria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(galeria);
        }

        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Galeria galeria = db.Galerias.Find(id);
            if (galeria == null)
            {
                return HttpNotFound();
            }
            //funcionesUtiles.QuitarTodas_LasImagenesDeRuta("/img/Galeria/" + galeria.Galeria_titulo, this.Server);
            //if(Directory.Exists(Server.MapPath("/img/Galeria/" + galeria.Galeria_titulo)))
            //{
            //    Directory.Delete(Server.MapPath("/img/Galeria/" + galeria.Galeria_titulo));
            //}
            var listaImagenes = galeria.Fotos_Galeria.ToList();
            foreach(var img in listaImagenes)
            {
                funcionesUtiles.QuitarImagen_Servidor(img.Fotos_galeria_imagenURL, this.Server);
                db.Fotos_Galerias.Remove(img);              
            }
            db.Galerias.Remove(galeria);
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
