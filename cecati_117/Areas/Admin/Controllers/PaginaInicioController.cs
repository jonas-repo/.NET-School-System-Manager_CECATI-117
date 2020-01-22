using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using cecati_117.Models;
using cecati_117.Areas.Admin.ClasesCompartidas;
    
namespace cecati_117.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PaginaInicioController : Controller
    {
        //private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private ApplicationDbContext db = new ApplicationDbContext();
        private FuncionesUtilesController funcionesUtiles = new FuncionesUtilesController();

        // GET: Admin/PaginaInicio
        public ActionResult Index(int? comp)
        {
            if(comp.Equals(1))
            {
                ViewBag.CambioDeContraseña = "LA CONTRASEÑA HA SIDO CAMBIADA CORRECTAMENTE";
            }
            List<InicioCarrusel_ListaDesplegada> lista_carruseles = new List<InicioCarrusel_ListaDesplegada>();
            var lista = db.InicioCarrusel.ToList();

            foreach (var carrusel in lista)
            {
                InicioCarrusel_ListaDesplegada carrusel_con_listaAprendizaje = new InicioCarrusel_ListaDesplegada();
                carrusel_con_listaAprendizaje.InicioCarrusel_ID = carrusel.InicioCarrusel_ID;
                carrusel_con_listaAprendizaje.InicioCarrusel_ImagenURL = carrusel.InicioCarrusel_ImagenURL;
                carrusel_con_listaAprendizaje.InicioCarrusel_MiniImagenUrl = carrusel.InicioCarrusel_MiniImagenUrl;
                carrusel_con_listaAprendizaje.InicioCarrusel_Titulo = carrusel.InicioCarrusel_Titulo;
                carrusel_con_listaAprendizaje.InicioCarrusel_ListaDeAprendizajes = funcionesUtiles.UnirString_EnArray(carrusel.InicioCarrusel_ListaAprendizaje,',');
                carrusel_con_listaAprendizaje.InicioCarrusel_Fecha = carrusel.InicioCarrusel_Fecha;
                lista_carruseles.Add(carrusel_con_listaAprendizaje);
            }
    
            
            return View(lista_carruseles.OrderByDescending(x => x.InicioCarrusel_Fecha));
        }


        // GET: Admin/PaginaInicio/Create
        public ActionResult Create(int? comp)
        {
            if(comp.Equals(1))
            {
                ViewBag.NoEsImagen = "Error por favor sube un archivo con extension de imagen .jpg,.png";
            }
            return View();
        }

        // POST: Admin/PaginaInicio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InicioCarrusel_ID,InicioCarrusel_Titulo")] InicioCarrusel inicioCarrusel,string[] actividades, HttpPostedFileBase imagen, HttpPostedFileBase miniImagen)
        {
            if(funcionesUtiles.Comprobar_SiEsImagen(imagen) && funcionesUtiles.Comprobar_SiEsImagen(miniImagen))
            {
                inicioCarrusel.InicioCarrusel_ImagenURL = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/InicioCarrusel/", this.Server);
                inicioCarrusel.InicioCarrusel_MiniImagenUrl = funcionesUtiles.AgregarImagen_Servidor(miniImagen, "/img/InicioCarrusel/mini_Imagen/", this.Server);
                inicioCarrusel.InicioCarrusel_ListaAprendizaje = funcionesUtiles.SepararArray_DeString(actividades, ",");
                inicioCarrusel.InicioCarrusel_Fecha = DateTime.Now;

                if (ModelState.IsValid)
                {
                    inicioCarrusel.InicioCarrusel_ID = Guid.NewGuid();
                    db.InicioCarrusel.Add(inicioCarrusel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Create", new { comp = 1 });
            }         
            return View(inicioCarrusel);
        }
     
        // GET: Admin/PaginaInicio/Edit/5
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
            InicioCarrusel inicioCarrusel = db.InicioCarrusel.Find(id);
            if (inicioCarrusel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListaCadenas = funcionesUtiles.UnirString_EnArray (inicioCarrusel.InicioCarrusel_ListaAprendizaje,',');
            return View(inicioCarrusel);
        }

        // POST: Admin/PaginaInicio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InicioCarrusel_ID,InicioCarrusel_Titulo,InicioCarrusel_Fecha,InicioCarrusel_ImagenURL,InicioCarrusel_MiniImagenUrl")] InicioCarrusel inicioCarrusel, string[] actividades, HttpPostedFileBase imagen, HttpPostedFileBase miniImagen)
        {

            if(imagen != null)
            {
                if(funcionesUtiles.Comprobar_SiEsImagen(imagen))
                {
                    funcionesUtiles.QuitarImagen_Servidor(inicioCarrusel.InicioCarrusel_ImagenURL, this.Server);
                    inicioCarrusel.InicioCarrusel_ImagenURL = funcionesUtiles.AgregarImagen_Servidor(imagen, "/img/InicioCarrusel/", this.Server);
                }
                else
                {
                    return RedirectToAction("Edit", new {id = inicioCarrusel.InicioCarrusel_ID, comp = 1 });
                }
                
            }
            //else
            //{
            //    inicioCarrusel.InicioCarrusel_ImagenURL = imagenAnterior[0];
            //}
            if (miniImagen != null)
            {
                if (funcionesUtiles.Comprobar_SiEsImagen(miniImagen))
                {
                    funcionesUtiles.QuitarImagen_Servidor(inicioCarrusel.InicioCarrusel_MiniImagenUrl, this.Server);
                    inicioCarrusel.InicioCarrusel_MiniImagenUrl = funcionesUtiles.AgregarImagen_Servidor(miniImagen, "/img/InicioCarrusel/mini_Imagen/", this.Server);
                }
                else
                {
                    return RedirectToAction("Edit", new { id = inicioCarrusel.InicioCarrusel_ID, comp = 1 });
                }

            }
            //else
            //{
            //    inicioCarrusel.InicioCarrusel_MiniImagenUrl = imagenAnterior[1];
            //}
            inicioCarrusel.InicioCarrusel_ListaAprendizaje = funcionesUtiles.SepararArray_DeString(actividades, ",");
            

            if (ModelState.IsValid)
            {
                db.Entry(inicioCarrusel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inicioCarrusel);
        }

        // GET: Admin/PaginaInicio/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    InicioCarrusel inicioCarrusel = db.InicioCarrusel.Find(id);
        //    if (inicioCarrusel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(inicioCarrusel);
        //}

        // POST: Admin/PaginaInicio/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InicioCarrusel inicioCarrusel = db.InicioCarrusel.Find(id);
            if (inicioCarrusel == null)
            {
                return HttpNotFound();
            }
            funcionesUtiles.QuitarImagen_Servidor(inicioCarrusel.InicioCarrusel_ImagenURL, this.Server);
            funcionesUtiles.QuitarImagen_Servidor(inicioCarrusel.InicioCarrusel_MiniImagenUrl, this.Server);
            db.InicioCarrusel.Remove(inicioCarrusel);
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
