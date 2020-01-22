using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactoController : Controller
    {
        // GET: Admin/Contacto
        
        public ActionResult Index()
        {
            return View();
        }
    }
}