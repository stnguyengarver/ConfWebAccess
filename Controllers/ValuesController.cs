using ConfWebAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ConfWebAccess.Controllers
{
    public class ValuesController : ApiController
    {
        private DB_A0B2A3_conferenceEntities db = new DB_A0B2A3_conferenceEntities();
        // GET api/values
        [BasicAuthentication]
        public IHttpActionResult Get()
        {
            // return new string[] { "value1", DateTime.Now.ToString() };
            var list =  db.lectures.Select(x=>new {id=x.id,Description=x.description }).ToList();
            return Ok(list);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        [BasicAuthentication]
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            var inputstr = value;

        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
