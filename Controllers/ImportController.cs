using ConfWebAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ConfWebAccess.Controllers
{
    public class ImportController : BaseController
    {
        private DB_A0B2A3_conferenceEntities db = new DB_A0B2A3_conferenceEntities();
        // GET: Import

            [Authorize]
        public ActionResult Index(int id)
        {
            ViewBag.id = id;

            var list = db.SysFilePaths.Where(x => x.LinkId == id);

            return View(list);
        }


        [Authorize]
        public ActionResult SaveImport(int id, HttpPostedFileBase upload)
        {
            Random rand = new Random();

            if(upload != null)
            {
                string FileName = "Presentation_" + rand.Next(1000, 9999).ToString() + "_" + System.IO.Path.GetFileName(upload.FileName);

                string namefromdb = "~/Temp/" + FileName;

                string path = HttpContext.Server.MapPath(namefromdb);

               // input.biographyfile = FileName;

                upload.SaveAs(path);

        


                    SysFilePath newupload = new SysFilePath();
                    newupload.FileName = FileName;
                newupload.FilePath = path;
                    newupload.FileType = "Presentation";
                newupload.UploadDate = DateTime.Now;
                    newupload.LinkId = id;

                    db.SysFilePaths.Add(newupload);
                    db.SaveChanges();
                RedirectToAction("Details", "confabstracts", new { id = id });
               

               
            }
     

            return RedirectToAction("Index",new { id = id });
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysFilePath sys = db.SysFilePaths.Find(id);
            if (sys == null)
            {
                return HttpNotFound();
            }
            return View(sys);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            SysFilePath sys = db.SysFilePaths.Find(id);
            db.SysFilePaths.Remove(sys);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = sys.LinkId });
        }
    }
}