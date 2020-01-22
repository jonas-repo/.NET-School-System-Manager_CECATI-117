using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Controllers
{
    public class SitemapController : Controller
    {
        // GET: Sitemap
        public ActionResult SitemapXML()
        {

            string xml = System.IO.File.ReadAllText(
                Server.MapPath("~/Content/sitemap.xml"));

            return Content(xml, "text/xml");
        }
    }
}