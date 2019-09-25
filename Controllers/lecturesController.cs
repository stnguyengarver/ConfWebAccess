using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConfWebAccess.Models;

namespace ConfWebAccess.Controllers
{
    public class lecturesController : BaseController
    {
        private DB_A0B2A3_conferenceEntities db = new DB_A0B2A3_conferenceEntities();

        // GET: lectures
      
        public ActionResult Index()
        {
            return View(db.lectures.ToList());
        }

        // GET: lectures/Details/5
      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lecture lecture = db.lectures.Find(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        // GET: lectures/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: lectures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( lecture lecture)
        {
            if (ModelState.IsValid)
            {
                db.lectures.Add(lecture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lecture);
        }

        // GET: lectures/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lecture lecture = db.lectures.Find(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        // POST: lectures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(lecture lecture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lecture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lecture);
        }

        // GET: lectures/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lecture lecture = db.lectures.Find(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        // POST: lectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            lecture lecture = db.lectures.Find(id);
            db.lectures.Remove(lecture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
