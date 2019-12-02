using ConfWebAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ConfWebAccess.Controllers
{
    public class AddClassController : ApiController
    {
        private DB_A0B2A3_conferenceEntities db = new DB_A0B2A3_conferenceEntities();
        // GET api/values
        [BasicAuthentication]
        public List<string> Get(int id,string userid)
        {
            // return new string[] { "value1", DateTime.Now.ToString() };
            try
            {
                var found = (from stdcls in db.studentclasses where stdcls.classid == id && stdcls.studentid == userid select stdcls).ToList();
                if (found.Count == 0)
                {
                    studentclass sl = new studentclass();
                    sl.classid = id;
                    sl.studentid = userid;
                   sl.scandate = DateTime.Now;
                    db.studentclasses.Add(sl);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
      

      

            var result =  (from cls in db.lectures
                    join stdcls in db.studentclasses on cls.id equals stdcls.classid
                    where stdcls.studentid == userid
                    select cls.description).ToList();

           return  result.ToList();
        }
    }
}
