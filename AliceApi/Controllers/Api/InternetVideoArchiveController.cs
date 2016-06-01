using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AliceApi.Controllers
{
    public class InternetVideoArchiveController : ApiController
    {
        // GET: api/InternetVideoArchive
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/InternetVideoArchive/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/InternetVideoArchive
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/InternetVideoArchive/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/InternetVideoArchive/5
        public void Delete(int id)
        {
        }
    }
}
