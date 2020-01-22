using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using cecati_117.Models;
using cecati_117.Areas.Admin.ClasesCompartidas;

namespace cecati_117.Controllers
{
    public class InicioCarruselController : Controller
    {
        //private Contexto_BaseDatos db = new Contexto_BaseDatos();
        private FuncionesUtilesController funcionesUtiles = new FuncionesUtilesController();
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: InicioCarrusel      
        public PartialViewResult CarruselPrincipal()
        {
            List<InicioCarrusel_ListaDesplegada> lista_carruseles = new List<InicioCarrusel_ListaDesplegada>();
            var lista = db.InicioCarrusel.ToList();

            foreach (var carrusel in lista)
            {
                InicioCarrusel_ListaDesplegada carrusel_con_listaAprendizaje = new InicioCarrusel_ListaDesplegada();
                carrusel_con_listaAprendizaje.InicioCarrusel_ID = carrusel.InicioCarrusel_ID;
                carrusel_con_listaAprendizaje.InicioCarrusel_ImagenURL = carrusel.InicioCarrusel_ImagenURL;
                carrusel_con_listaAprendizaje.InicioCarrusel_MiniImagenUrl = carrusel.InicioCarrusel_MiniImagenUrl;
                carrusel_con_listaAprendizaje.InicioCarrusel_Titulo = carrusel.InicioCarrusel_Titulo;
                carrusel_con_listaAprendizaje.InicioCarrusel_ListaDeAprendizajes = funcionesUtiles.UnirString_EnArray(carrusel.InicioCarrusel_ListaAprendizaje, ',');
                carrusel_con_listaAprendizaje.InicioCarrusel_Fecha = carrusel.InicioCarrusel_Fecha;
                lista_carruseles.Add(carrusel_con_listaAprendizaje);
            }
            return PartialView(lista_carruseles.OrderByDescending(x => x.InicioCarrusel_Fecha));
        }
    }
}