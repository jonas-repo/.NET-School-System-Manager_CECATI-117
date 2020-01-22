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
    public class Fotos_galeriaController : Controller
    {
        // private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();
        private FuncionesUtilesController funcionesUtiles = new FuncionesUtilesController();

        public PartialViewResult FotosAsociadas(Guid id)
        {
            if (id == null)
            {
                ViewBag.nulo = true;
            }
            var fotos_galeria = db.Fotos_Galerias.Where(x => x.Id_Galeria.Galeria_ID.Equals(id)).OrderByDescending(z => z.Fotos_galeria_fecha).ToList();
            if (fotos_galeria.Count.Equals(0))
            {
                ViewBag.vacia = true;
            }
            else
            {
                ViewBag.fotos = fotos_galeria;
            }
            return PartialView();
        }


        [HttpPost]
        public JsonResult Subida_Imagenes_Galeria(IEnumerable<HttpPostedFileBase> imagenes, string titulo, string autor, Guid id_galeriaAsociada)
        {
            //más informacion sobre el plugin para subir imagenes http://plugins.krajee.com/file-input-ajax-demo/1 // se utiliza ajax
            Galeria galeria = db.Galerias.Find(id_galeriaAsociada);


            foreach (var imagen in imagenes)
            {
                Fotos_galeria fotos_galeria = new Fotos_galeria
                {
                    Fotos_galeriaID = Guid.NewGuid()
                };
                fotos_galeria.Fotos_galeria_autor = autor;
                fotos_galeria.Fotos_galeria_titulo = titulo;
                fotos_galeria.Fotos_galeria_imagenURL = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/Galeria/", this.Server);
                fotos_galeria.Id_Galeria = galeria;
                fotos_galeria.Fotos_galeria_fecha = DateTime.Now;
                db.Fotos_Galerias.Add(fotos_galeria);
                db.SaveChanges();
            }

            //if (Directory.Exists(Server.MapPath("/img/Galeria/"+galeria.Galeria_titulo+"")))
            //{
            //    foreach(var imagen in imagenes)
            //    {
            //        Fotos_galeria fotos_galeria = new Fotos_galeria
            //        {
            //            Fotos_galeriaID = Guid.NewGuid()
            //        };
            //        fotos_galeria.Fotos_galeria_autor = autor;
            //        fotos_galeria.Fotos_galeria_titulo = titulo;
            //        fotos_galeria.Fotos_galeria_imagenURL = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/Galeria/" + galeria.Galeria_titulo + "/", this.Server);
            //        fotos_galeria.Id_Galeria = galeria;
            //        fotos_galeria.Fotos_galeria_fecha = DateTime.Now;
            //        db.Fotos_Galerias.Add(fotos_galeria);
            //        db.SaveChanges();
            //    }
            //}
            //else
            //{
            //    Directory.CreateDirectory(Server.MapPath("/img/Galeria/" + galeria.Galeria_titulo + ""));
            //    foreach (var imagen in imagenes)
            //    {
            //        Fotos_galeria fotos_galeria = new Fotos_galeria
            //        {
            //            Fotos_galeriaID = Guid.NewGuid()
            //        };
            //        fotos_galeria.Fotos_galeria_autor = autor;
            //        fotos_galeria.Fotos_galeria_titulo = titulo;
            //        fotos_galeria.Fotos_galeria_imagenURL = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/Galeria/" + galeria.Galeria_titulo + "/", this.Server);
            //        fotos_galeria.Id_Galeria = galeria;
            //        fotos_galeria.Fotos_galeria_fecha = DateTime.Now;
            //        db.Fotos_Galerias.Add(fotos_galeria);
            //        db.SaveChanges();
            //    }

            //}
            
            return Json("file uploaded succesfully");
        }




        // GET: Admin/Fotos_galeria/Create
        public ActionResult Create(Guid id,int? comp)
        {
            if(comp.Equals(1))
            {
                ViewBag.NoImagen = "Porfavor sube solo archivos .png o .jpg";
            }
            if(comp.Equals(2))
            {
                ViewBag.NoStrings = "Porfavor no dejes el titulo ni el autor vacios";
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Galeria galeria_asignada = db.Galerias.Find(id);
            if (galeria_asignada == null)
            {
                return HttpNotFound();
            }          
            ViewBag.galeria_nombre = galeria_asignada.Galeria_titulo;
            ViewBag.galeria_Id = galeria_asignada.Galeria_ID;
            return View();
        }

        // POST: Admin/Fotos_galeria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Fotos_galeriaID,Fotos_galeria_titulo,Fotos_galeria_autor,Fotos_galeria_imagenURL")] Fotos_galeria fotos_galeria)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        fotos_galeria.Fotos_galeriaID = Guid.NewGuid();
        //        db.Fotos_Galerias.Add(fotos_galeria);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(fotos_galeria);
        //}

        // GET: Admin/Fotos_galeria/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos_galeria fotos_galeria = db.Fotos_Galerias.Find(id);
            if (fotos_galeria == null)
            {
                return HttpNotFound();
            }
            return View(fotos_galeria);
        }

        // POST: Admin/Fotos_galeria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Fotos_galeriaID,Fotos_galeria_titulo,Fotos_galeria_autor,Fotos_galeria_imagenURL")] Fotos_galeria fotos_galeria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fotos_galeria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fotos_galeria);
        }

        // GET: Admin/Fotos_galeria/Delete/5
        public ActionResult DeleteConfirmed(Guid id, Guid id_asociado)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos_galeria fotos_galeria = db.Fotos_Galerias.Find(id);
            if (fotos_galeria == null)
            {
                return HttpNotFound();
            }
            funcionesUtiles.QuitarImagen_Servidor(fotos_galeria.Fotos_galeria_imagenURL, this.Server);
            db.Fotos_Galerias.Remove(fotos_galeria);
            db.SaveChanges();
            return RedirectToAction("Create",new { id=id_asociado});
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
