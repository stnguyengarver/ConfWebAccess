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
    public class presentersController : Controller
    {
        private DB_A0B2A3_conferenceEntities db = new DB_A0B2A3_conferenceEntities();

        // GET: presenters
        public ActionResult Index()
        {
            var presenters = db.presenters.Include(p => p.confabstract);
            return View(presenters.ToList());
        }

        // GET: presenters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presenter presenter = db.presenters.Find(id);
            if (presenter == null)
            {
                return HttpNotFound();
            }
            return View(presenter);
        }

        // GET: presenters/Create
        public ActionResult Create()
        {
            ViewBag.abstractid = new SelectList(db.confabstracts, "id", "userid");
            return View();
        }

        // POST: presenters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,abstractid,fullname,organization,bio")] presenter presenter)
        {
            if (ModelState.IsValid)
            {
                db.presenters.Add(presenter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.abstractid = new SelectList(db.confabstracts, "id", "userid", presenter.abstractid);
            return View(presenter);
        }

        // GET: presenters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presenter presenter = db.presenters.Find(id);
            if (presenter == null)
            {
                return HttpNotFound();
            }
            ViewBag.abstractid = new SelectList(db.confabstracts, "id", "userid", presenter.abstractid);
            return View(presenter);
        }

        // POST: presenters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,abstractid,fullname,organization,bio")] presenter presenter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presenter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.abstractid = new SelectList(db.confabstracts, "id", "userid", presenter.abstractid);
            return View(presenter);
        }

        // GET: presenters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presenter presenter = db.presenters.Find(id);
            if (presenter == null)
            {
                return HttpNotFound();
            }
            return View(presenter);
        }

        // POST: presenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            presenter presenter = db.presenters.Find(id);
            db.presenters.Remove(presenter);
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
