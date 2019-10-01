using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConfWebAccess.Controllers
{
    public class mapsController : Controller
    {
        // GET: maps
        public ActionResult OverView()
        {
            return View();
        }

        public ActionResult Exhibitor()
        {
            return View();
        }

        public ActionResult Classroom()
        {
            return View();
        }
    }
}