using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConfWebAccess.Controllers
{

    public class classesController : BaseController
    {
        // GET: classes
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Attended()
        {
            return View();
        }

        public ActionResult Intinerary()
        {
            return View();
        }

        [Authorize]
        public ActionResult Survey()
        {
            return View();
        }


    }
}