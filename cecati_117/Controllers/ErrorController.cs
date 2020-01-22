using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int error = 0)
        {
            // para saber más sobre la pagina de error visita http://anexsoft.com/p/99/implementando-un-custom-error-page-con-asp-net-mvc //
            switch (error)
            {
                case 505:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Vuelve a intentarlo más tarde";
                    ViewBag.Number = error;
                    break;

                case 404:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    ViewBag.Number = error;
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Vuelve a intentarlo más tarde";
                    ViewBag.Number = error;
                    break;
            }

            return View("~/views/error/_ErrorPage.cshtml");
        }
    }
}