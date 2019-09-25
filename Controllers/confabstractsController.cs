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
    public class confabstractsController : BaseController
    {
        private DB_A0B2A3_conferenceEntities db = new DB_A0B2A3_conferenceEntities();

        // GET: confabstracts
        public ActionResult Index()
        {

            ViewBag.Admin = false;

           if(CurrentUser.SysUserType.TypeName == "SysAdmin")
            {
                ViewBag.Admin = true;
                return View(db.confabstracts.ToList());
            }else
            {
                return View(db.confabstracts.Where(x=>x.userid == CurrentUser.Id).ToList());
            }
            
        }

 
        // GET: confabstracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            confabstract confabstract = db.confabstracts.Find(id);
            if (confabstract == null)
            {
                return HttpNotFound();
            }
            return View(confabstract);
        }



        // POST: confabstracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            confabstract confabstract = new confabstract();
            confabstract.title = "title...";
            confabstract.status = "In Progress";
                confabstract.userid = CurrentUser.Id;
                db.confabstracts.Add(confabstract);
                db.SaveChanges();

                confabstract newabs = db.confabstracts.Where(x => x.userid == CurrentUser.Id).OrderByDescending(x=>x.id).FirstOrDefault();

                presenter newpres = new presenter();
                newpres.fullname = CurrentUser.FirstName + " " + CurrentUser.LastName;
                newpres.abstractid = newabs.id;

                db.presenters.Add(newpres);
                db.SaveChanges();

                return RedirectToAction("Edit",new {id=newabs.id });
          
        }

        // GET: confabstracts/Edit/5
        public ActionResult Edit(int? id)
        {
            var catlist = db.categorylists.ToList();

            ViewBag.catlist = new SelectList(catlist, "description", "description");


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            confabstract confabstract = db.confabstracts.Include(b => b.presenters).Where(x=>x.id == id).FirstOrDefault();
            if (confabstract == null)
            {
                return HttpNotFound();
            }
            return View(confabstract);
        }

        // POST: confabstracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( confabstract confabstract,presenter[] Presenters, string action)
        {
            var catlist = db.categorylists.ToList();

            ViewBag.catlist = new SelectList(catlist, "description", "description");



            confabstract conf = db.confabstracts.Find(confabstract.id);
                conf.title = confabstract.title;
                conf.category = confabstract.category;
                conf.objective1 = confabstract.objective1;
            conf.objective2 = confabstract.objective2;
            conf.objective3 = confabstract.objective3;
            conf.summary = confabstract.summary;
   

            db.SaveChanges();

                    if(Presenters != null)
                    foreach(presenter pres in Presenters)
                    {
                        presenter p = db.presenters.Find(pres.id);
                        p.fullname = pres.fullname;
                        p.organization = pres.organization;
                        p.bio = pres.bio;
                       
                    }

                    db.SaveChanges();

                 
          

            if(action == "Add Presenter")
            {

                presenter newpresenter = new presenter();
                newpresenter.abstractid = confabstract.id;
                newpresenter.fullname = "please enter full name";
                db.presenters.Add(newpresenter);
                db.SaveChanges();

            }

            if (action == "Submit")
            {
             
                conf.status = "Submitted";
                conf.submitdate = DateTime.Now.ToShortDateString();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

     


            //default action 
            confabstract confabstract2 = db.confabstracts.Include(b => b.presenters).Where(x => x.id == confabstract.id).FirstOrDefault();
                return View(confabstract2);
        

         
        }

        // GET: confabstracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            confabstract confabstract = db.confabstracts.Find(id);
            if (confabstract == null)
            {
                return HttpNotFound();
            }
            return View(confabstract);
        }

        // POST: confabstracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            //first , need to delete the child records : presenter
            var pres = db.presenters.Where(x => x.abstractid == id).ToList();

            if(pres != null)
            foreach(presenter p in pres)
            {
                    presenter presenter = db.presenters.Find(p.id);
                    db.presenters.Remove(presenter);
            }

            db.SaveChanges();

            confabstract confabstract = db.confabstracts.Find(id);
            db.confabstracts.Remove(confabstract);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Revert(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            confabstract confabstract = db.confabstracts.Find(id);
            if (confabstract == null)
            {
                return HttpNotFound();
            }
            return View(confabstract);
        }


        [HttpPost, ActionName("Revert")]
        [ValidateAntiForgeryToken]
        public ActionResult Revert(int id)
        {
           

            confabstract confabstract = db.confabstracts.Find(id);
   
            confabstract.status = "In Progress";

            db.SaveChanges();
            return RedirectToAction("Index");



        }

        [HttpPost, ActionName("Accept")]
        [ValidateAntiForgeryToken]
        public ActionResult Accept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            confabstract confabstract = db.confabstracts.Find(id);
            if (confabstract == null)
            {
                return HttpNotFound();
            }
            return View(confabstract);
        }


        public ActionResult Accept(int id)
        {


            confabstract confabstract = db.confabstracts.Find(id);
         
            confabstract.status = "Accepted";

            db.SaveChanges();
            return RedirectToAction("Index");



        }





        public ActionResult DeletePresenter(int id, int abstractid)
        {
            presenter presenter = db.presenters.Find(id);
            db.presenters.Remove(presenter);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id=abstractid});
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
