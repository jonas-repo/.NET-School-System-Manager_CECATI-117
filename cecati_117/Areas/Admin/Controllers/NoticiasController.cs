using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using cecati_117.Models;
using cecati_117.Areas.Admin.ClasesCompartidas;

namespace cecati_117.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NoticiasController : Controller
    {
        //private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();
        private FuncionesUtilesController funcionesUtiles = new FuncionesUtilesController();

        // GET: Admin/Noticias
        public ActionResult Index()
        {
            return View(db.Noticias.OrderByDescending(x => x.Noticias_Fecha).ToList());
        }

        // GET: Admin/Noticias/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
            {
                return HttpNotFound();
            }
            return View(noticias);
        }

        // GET: Admin/Noticias/Create
        public ActionResult Create(int? comp)
        {
            if (comp.Equals(1))
            {
                ViewBag.NoEsImagen = "Error por favor sube un archivo con extension de imagen .jpg,.png";
            }
            return View();
        }

        // POST: Admin/Noticias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Noticias_ID,Noticias_Titulo")] Noticias noticias, HttpPostedFileBase imagen, string __descr)
        {
            if (funcionesUtiles.Comprobar_SiEsImagen(imagen))
            {
                noticias.Noticias_ImagenURL = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/noticias_recientes/", this.Server);
                noticias.Noticias_Descripcion = __descr;
                noticias.Noticias_Fecha = DateTime.Now;
                if (ModelState.IsValid)
                {
                    noticias.Noticias_ID = Guid.NewGuid();
                    db.Noticias.Add(noticias);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Noticias");
                }
            }
            else
            {
                return RedirectToAction("Create", new { comp = 1 });
            }
            return View(noticias);
        }

        // GET: Admin/Noticias/Edit/5
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
            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
            {
                return HttpNotFound();
            }
            ViewBag.UrlImagenAnterior = noticias.Noticias_ImagenURL;
            return View(noticias);
        }

        // POST: Admin/Noticias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Noticias_ID,Noticias_Fecha,Noticias_Titulo")] Noticias noticias, HttpPostedFileBase imagen, string imagenAnterior, string __descr)
        {
            if (imagen != null)
            {
                if (funcionesUtiles.Comprobar_SiEsImagen(imagen))
                {
                    funcionesUtiles.QuitarImagen_Servidor(imagenAnterior, this.Server);
                    noticias.Noticias_ImagenURL = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/noticias_recientes/", this.Server);
                }
                else
                {
                    return RedirectToAction("Edit", new { id = noticias.Noticias_ID, comp = 1 });
                }
            }
            else
            {
                noticias.Noticias_ImagenURL = imagenAnterior;
            }
            if (ModelState.IsValid)
            {
                noticias.Noticias_Descripcion = __descr;
                db.Entry(noticias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Noticias");
            }
            return View(noticias);
        }




        // POST: Admin/Noticias/Delete/5
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
            {
                return HttpNotFound();
            }
            funcionesUtiles.QuitarImagen_Servidor(noticias.Noticias_ImagenURL, this.Server);
            db.Noticias.Remove(noticias);
            db.SaveChanges();
            return RedirectToAction("Index", "PaginaInicio");
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
