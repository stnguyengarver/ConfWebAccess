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
    [Authorize]
    public class surveysController : BaseController
    {
        private DB_A0B2A3_conferenceEntities db = new DB_A0B2A3_conferenceEntities();

        // GET: surveys
        public ActionResult Index()
        {
            var surveys = db.surveys.Include(s => s.AspNetUser).Include(s => s.lecture);
            return View(surveys.ToList());
        }

        // GET: surveys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            survey survey = db.surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // GET: surveys/Create
        public ActionResult Create(int? id)
        {
            ViewBag.userid = CurrentUser.Id;
            ViewBag.lectureid = id;
            return View();
        }

        // POST: surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userid,lectureid,Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9")] survey survey)
        {
            if (ModelState.IsValid)
            {
                db.surveys.Add(survey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", survey.userid);
            ViewBag.lectureid = new SelectList(db.lectures, "id", "code", survey.lectureid);
            return View(survey);
        }

        // GET: surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            survey survey = db.surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", survey.userid);
            ViewBag.lectureid = new SelectList(db.lectures, "id", "code", survey.lectureid);
            return View(survey);
        }

        // POST: surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userid,lectureid,Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9")] survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", survey.userid);
            ViewBag.lectureid = new SelectList(db.lectures, "id", "code", survey.lectureid);
            return View(survey);
        }

        // GET: surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            survey survey = db.surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            survey survey = db.surveys.Find(id);
            db.surveys.Remove(survey);
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
