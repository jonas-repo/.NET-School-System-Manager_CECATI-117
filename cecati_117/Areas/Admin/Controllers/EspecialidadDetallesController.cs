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
    public class EspecialidadDetallesController : Controller
    {
        //private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();
        private FuncionesUtilesController funcionesUtiles = new FuncionesUtilesController();


        // GET: Admin/EspecialidadDetalles
        public ActionResult Index()
        {           
            return View(db.EspecialidadDetalles.ToList());
        }

        // GET: Admin/EspecialidadDetalles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecialidadDetalles especialidadDetalles = db.EspecialidadDetalles.Find(id);
            if (especialidadDetalles == null)
            {
                return HttpNotFound();
            }
            return View(especialidadDetalles);
        }

        // GET: Admin/EspecialidadDetalles/Create
        public ActionResult Create(Guid id,int? comp)
        {
            if (comp.Equals(1))
            {
                ViewBag.NoEsImagen = "Error por favor sube un archivo con extension de imagen .jpg,.png";
            }
            ViewBag.IdAsociado = id;
            ViewBag.NombreEspecialidad_Asociar = db.ListaEspecialidades.Find(id).ListaEspecialidades_especialidad;
            return View();
        }

        // POST: Admin/EspecialidadDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EspecialidadDetalles_ID,EspecialidadDetalles_titulo")] EspecialidadDetalles especialidadDetalles, HttpPostedFileBase imagen, Guid IdEspecialidad_Asociado, string[] actividades, string __descr)
        {
            if(funcionesUtiles.Comprobar_SiEsImagen(imagen))
            {
                especialidadDetalles.EspecialidadDetalles_Imagen = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/PaginasDeEspecialidad/", this.Server);
                especialidadDetalles.EspecialidadDetalles_ListaAprendizaje = funcionesUtiles.SepararArray_DeString(actividades, ",");
                especialidadDetalles.EspecialidadDetalles_descripcion = __descr;
                especialidadDetalles.Id_Especialidad = db.ListaEspecialidades.Find(IdEspecialidad_Asociado);
                
                if (ModelState.IsValid)
                {
                    especialidadDetalles.EspecialidadDetalles_ID = Guid.NewGuid();
                    db.EspecialidadDetalles.Add(especialidadDetalles);
                    db.SaveChanges();
                    return RedirectToAction("Index","ListaEspecialidades");
                }
            }
            else
            {
                return RedirectToAction("Create", new { comp = 1 });
            }
            return View(especialidadDetalles);
        }

        // GET: Admin/EspecialidadDetalles/Edit/5
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
            EspecialidadDetalles especialidadDetalles = db.EspecialidadDetalles.Find(id);
            if (especialidadDetalles == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListaCadenas = funcionesUtiles.UnirString_EnArray(especialidadDetalles.EspecialidadDetalles_ListaAprendizaje, ',');
            ViewBag.UrlImagenAnterior = especialidadDetalles.EspecialidadDetalles_Imagen;
            ViewBag.IdAsociado = especialidadDetalles.Id_Especialidad.ListaEspecialidades_ID;
            return View(especialidadDetalles);
        }

        // POST: Admin/EspecialidadDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EspecialidadDetalles_ID,EspecialidadDetalles_titulo")] EspecialidadDetalles especialidadDetalles, HttpPostedFileBase imagen, Guid IdEspecialidad_Asociado, string[] actividades, string imagenAnterior, string __descr)
        {
            if (imagen != null)
            {
                if (funcionesUtiles.Comprobar_SiEsImagen(imagen))
                {
                    funcionesUtiles.QuitarImagen_Servidor(imagenAnterior, this.Server);
                    especialidadDetalles.EspecialidadDetalles_Imagen = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/PaginasDeEspecialidad/", this.Server);
                }
                else
                {
                    return RedirectToAction("Edit", new { id = especialidadDetalles.EspecialidadDetalles_ID, comp = 1 });
                }

            }
            else
            {
                especialidadDetalles.EspecialidadDetalles_Imagen = imagenAnterior;
            }

            especialidadDetalles.Id_Especialidad = db.ListaEspecialidades.Find(IdEspecialidad_Asociado);
            especialidadDetalles.EspecialidadDetalles_ListaAprendizaje = funcionesUtiles.SepararArray_DeString(actividades, ",");
            especialidadDetalles.EspecialidadDetalles_descripcion = __descr;

            if (ModelState.IsValid)
            {
                db.Entry(especialidadDetalles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","ListaEspecialidades");
            }
            return View(especialidadDetalles);
        }

        // GET: Admin/EspecialidadDetalles/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EspecialidadDetalles especialidadDetalles = db.EspecialidadDetalles.Find(id);
        //    if (especialidadDetalles == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(especialidadDetalles);
        //}

        // POST: Admin/EspecialidadDetalles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecialidadDetalles especialidadDetalles = db.EspecialidadDetalles.Find(id);
            if (especialidadDetalles == null)
            {
                return HttpNotFound();
            }
            funcionesUtiles.QuitarImagen_Servidor(especialidadDetalles.EspecialidadDetalles_Imagen, this.Server);
            db.EspecialidadDetalles.Remove(especialidadDetalles);
            db.SaveChanges();
            return RedirectToAction("Index", "ListaEspecialidades");
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
