using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConfWebAccess.Controllers
{
    public class agendaController : Controller
    {
        // GET: agenda
        public ActionResult Schedule()
        {
            return View();
        }


        public ActionResult SessionTracks()
        {
            return View();
        }
    }
}