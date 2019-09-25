using ConfWebAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConfWebAccess.Controllers
{
    public class ImportController : BaseController
    {
        private DB_A0B2A3_conferenceEntities db = new DB_A0B2A3_conferenceEntities();
        // GET: Import

            [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult SaveImport(confabstract input, HttpPostedFileBase upload, HttpPostedFileBase upload2)
        {
            Random rand = new Random();

            if(upload != null)
            {
                string FileName = "Biography_" + rand.Next(1000, 9999).ToString() + "_" + System.IO.Path.GetFileName(upload.FileName);

                string namefromdb = "~/Temp/" + FileName;

                string path = HttpContext.Server.MapPath(namefromdb);

               // input.biographyfile = FileName;

                upload.SaveAs(path);

                 FileName = "Abstract_" + rand.Next(1000, 9999).ToString() + "_" + System.IO.Path.GetFileName(upload2.FileName);

                 namefromdb = "~/Temp/" + FileName;

                 path = HttpContext.Server.MapPath(namefromdb);

             //   input.abstractfile = FileName;

                upload.SaveAs(path);


                if (ModelState.IsValid)
                {
                    input.userid = CurrentUser.Id;
                    db.confabstracts.Add(input);
                    db.SaveChanges();
                    return View("Success");
                }

                return View("Success");
            }else
            {
                if (ModelState.IsValid)
                {
                    input.userid = CurrentUser.Id;
                    db.confabstracts.Add(input);
                    db.SaveChanges();
                    return View("Success");
                }
            }
     

            return View("Index");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}