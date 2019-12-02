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
    public class categorylistsController : Controller
    {
        private DB_A0B2A3_conferenceEntities db = new DB_A0B2A3_conferenceEntities();

        // GET: categorylists
        public ActionResult Index()
        {
            return View(db.categorylists.ToList());
        }

        // GET: categorylists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categorylist categorylist = db.categorylists.Find(id);
            if (categorylist == null)
            {
                return HttpNotFound();
            }
            return View(categorylist);
        }

        // GET: categorylists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: categorylists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,description")] categorylist categorylist)
        {
            if (ModelState.IsValid)
            {
                db.categorylists.Add(categorylist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categorylist);
        }

        // GET: categorylists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categorylist categorylist = db.categorylists.Find(id);
            if (categorylist == null)
            {
                return HttpNotFound();
            }
            return View(categorylist);
        }

        // POST: categorylists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,description")] categorylist categorylist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorylist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorylist);
        }

        // GET: categorylists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categorylist categorylist = db.categorylists.Find(id);
            if (categorylist == null)
            {
                return HttpNotFound();
            }
            return View(categorylist);
        }

        // POST: categorylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categorylist categorylist = db.categorylists.Find(id);
            db.categorylists.Remove(categorylist);
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
