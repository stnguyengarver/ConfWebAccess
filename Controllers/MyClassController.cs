using ConfWebAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Web.Mvc;

namespace ConfWebAccess.Controllers
{
    public class MyClassController : BaseController
    {
        private DB_A0B2A3_conferenceEntities db = new DB_A0B2A3_conferenceEntities();

        // GET: lectures
        [Authorize]
        public ActionResult Index()
        {
            if(CurrentUser != null)
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

        [Authorize]
        public ActionResult Delete(int id)
        {
            return View(context.studentclasses.Find(id));
        }


        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var target = context.studentclasses.Find(id);
                context.studentclasses.Remove(target);
                context.SaveChanges();

                // TempData["UserMessage"] = new UserMessage { CssClassName = "alert-success", Title = "Success!", Message = "Record Deleted." };
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { message = ex.Message, innermessage = ex.InnerException.Message });
            }
        }

    }
}
