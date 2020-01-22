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
    public class SubtemaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Subtema
        public ActionResult Index()
        {
            return View(db.ListaEspecialidades.ToList());
        }



        // GET: Admin/Subtema/Create
        public ActionResult Create(Guid id)
        {
            ViewBag.Tema = db.Tema.Find(id);
            if (ViewBag.Tema == null)
            {
                return Redirect("~/Index/Error/404");
            }

            return View();
        }

        [HttpPost]
        public JsonResult Crear(Subtema model, Guid id)
        {
            ResponseModel responseModel = new ResponseModel();
            model.Tema = db.Tema.Find(id);
            model.Subtema_id = Guid.NewGuid();

            if (model.Tema == null)
            {
                RedirectToAction("Index");
            }
            else
            {
                if (String.IsNullOrEmpty(model.Subtema_nombre) || String.IsNullOrWhiteSpace(model.Subtema_nombre))
                {
                    responseModel.function = "CargarTabla()";
                    responseModel.message = "No dejes campos vacios";                  
                    return Json(responseModel);
                }

                db.Subtema.Add(model);
                db.SaveChanges();
                responseModel.SetResponse(true);
                responseModel.function = "CargarTabla()";
                responseModel.message = "Cambios guardados correctamente";
            }
            return Json(responseModel);

        }

        public PartialViewResult TablaSubtemas(Guid id)
        {
            var tema = db.Tema.Find(id);
            if (tema == null)
            {
                ViewBag.Subtemas = null;
                return PartialView();
            }
            else
            {
                ViewBag.Subtemas = tema.Subtemas;
                return PartialView();
            }
        }



        // GET: Admin/Subtema/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subtema subtema = db.Subtema.Find(id);
            if (subtema == null)
            {
                return HttpNotFound();
            }
            return View(subtema);
        }

        // POST: Admin/Subtema/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Subtema subtema)
        {        

                var sub = db.Subtema.Find(subtema.Subtema_id);
                if(sub == null)
                {
                    return HttpNotFound();
                }
                db.Configuration.ValidateOnSaveEnabled = false;
                sub.Subtema_nombre = subtema.Subtema_nombre;
                db.Entry(sub).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Create", new { id = sub.Tema.Tema_id });            
        }
        public ActionResult DeleteConfirmed(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subtema subtema = db.Subtema.Find(id);
            if (subtema == null)
            {
                return HttpNotFound();
            }
            Guid guid_tema = subtema.Tema.Tema_id;
           


            db.Subtema.Remove(subtema);
            db.SaveChanges();
            return RedirectToAction("Create", new { id = guid_tema });
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
