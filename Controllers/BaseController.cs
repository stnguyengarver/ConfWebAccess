using ConfWebAccess.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ConfWebAccess.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class BaseController : Controller
    {
        public DB_A0B2A3_conferenceEntities context;
        public BaseController()
        {
            context = new DB_A0B2A3_conferenceEntities();
   


        }



        public string GetUserName()
        {
            string userid = User.Identity.GetUserId();
            DB_A0B2A3_conferenceEntities context = new DB_A0B2A3_conferenceEntities();
            var s = context.AspNetUsers.Find(userid);
            return s.FirstName + " " + s.LastName;
        }




        public AspNetUser CurrentUser
        {
            get
            {
                if (User != null)
                {
                    string userid = User.Identity.GetUserId();
                    DB_A0B2A3_conferenceEntities context = new DB_A0B2A3_conferenceEntities();
                    return context.AspNetUsers.Find(userid);
                }
                else return null;
       


            }


        }


     
    }
}
