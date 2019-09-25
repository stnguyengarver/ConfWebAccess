using ConfWebAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace ConfWebAccess.Controllers
{
    public class UsersController : BaseController
    {


        ApplicationDbContext appcontext;

        public UsersController()
        {


            appcontext = new ApplicationDbContext();
        }


        // GET: Users


        [Authorize]
        public ActionResult Index(string sort, bool? showinactive)
        {
            ViewBag.sort = sort;


            if (showinactive.HasValue)
            {
                if ((bool)showinactive)
                {

                    ViewBag.showinactive = true;
                    return View(context.AspNetUsers.Where(x => !x.Active).ToList());
                }
                else
                {

                    ViewBag.showinactive = true;
                    return View(context.AspNetUsers.Where(x => x.Active).ToList());
                }
            }
            else
            {

                ViewBag.showinactive = false;
                return View(context.AspNetUsers.Where(x => x.Active).ToList());
            }


        }


        [Authorize]
        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {

            if (id == null) return RedirectToAction("Index", id);


            // prepopulat roles for the view dropdown
            // var list = context.AspNetRoles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

            // ViewBag.Roles = new SelectList(appcontext.Roles.Where(u => !u.Name.Contains("Admin")).ToList(), "Name", "Name");

            var model = context.AspNetUsers.Find(id);
          ViewBag.UserTypes = new SelectList(context.SysUserTypes.ToList(), "Id", "Description");
            if (model.LockoutEndDateUtc > DateTime.Now.AddYears(1))
            {
                ViewBag.UserLocked = true;
            }
            else
            {
                ViewBag.UserLocked = false;
            }
            //ViewBag.Locations = new SelectList(context.RefLocations.ToList(), "Id", "Description");

            return View(model);
        }




        [Authorize]
        public ActionResult Unlock(string id)
        {
            var currentuser = context.AspNetUsers.Find(id);
            currentuser.LockoutEndDateUtc = null;
            context.SaveChanges();

           // Utility.SendEmailToUser(currentuser.Email, "VTT DMS Account Status", "Your account has been unlocked / activated by the Administrator.  Click here to login: http://www.VTTdms.com ");

           // TempData["UserMessage"] = new UserMessage { CssClassName = "alert-success", Title = "Success!", Message = "User account has been unlocked." };
            return RedirectToAction("Index");
        }




        [Authorize]
        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, AspNetUser user, bool UserLocked)
        {
            try
            {

                bool sendemail = false;

                // TODO: Add update logic here
                var currentuser = context.AspNetUsers.Find(id);
                currentuser.Email = user.Email;
                currentuser.UserName = user.Email;
                currentuser.EmailConfirmed = user.EmailConfirmed;
                currentuser.FirstName = user.FirstName;
                currentuser.LastName = user.LastName;
                currentuser.UserTypeId = user.UserTypeId;
               currentuser.Title = user.Title;
                currentuser.Active = user.Active;

                if (currentuser.LockoutEndDateUtc != null) //user is locked
                {
                    currentuser.LockoutEndDateUtc = null;
                    //send unlock email but only after it is saved.. 
                    sendemail = true;
                }
                else
                {
                    if (UserLocked)
                    {
                        currentuser.LockoutEndDateUtc = DateTime.Now.AddYears(10);
                    }
                }


                context.SaveChanges();


               // TempData["UserMessage"] = new UserMessage { CssClassName = "alert-success", Title = "Success!", Message = "Data Saved." };


                //returns back to list
                return RedirectToAction("Index");
            }
            catch
            {

                //error occur , bring user back to edit page
                var model = context.AspNetUsers.Find(id);
               // ViewBag.UserTypes = new SelectList(context.SysUserTypes.ToList(), "Id", "Description");
               // ViewBag.Locations = new SelectList(context.stores.ToList(), "Id", "storename");
              //  TempData["UserMessage"] = new UserMessage { CssClassName = "alert-danger", Title = "Error!", Message = "Operation Failed." };
                return View(model);
            }
        }


        [Authorize]
        public ActionResult Delete(string id)
        {
            return View(context.AspNetUsers.Find(id));
        }

        // POST: Users/Delete/5
        [Authorize]
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var target = context.AspNetUsers.Find(id);
                context.AspNetUsers.Remove(target);
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
