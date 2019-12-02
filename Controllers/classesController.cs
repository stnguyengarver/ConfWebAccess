using ConfWebAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConfWebAccess.Controllers
{

    public class classesController : BaseController
    {
        private DB_A0B2A3_conferenceEntities db = new DB_A0B2A3_conferenceEntities();
        // GET: classes
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Attended()
        {
            if (CurrentUser != null)
            {
                IEnumerable<studentclass> result = (from cls in db.lectures
                                                    join stdcls in db.studentclasses on cls.id equals stdcls.classid
                                                    where stdcls.studentid == CurrentUser.Id
                                                    select stdcls).ToList();

                return View(result);
            }
            else
            {
                return View();
            }
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