using Product_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Product_Management.Controllers
{
    public class ValuesController : ApiController
    {
        Users_context context = new Users_context();
        // GET api/values
        public IEnumerable<Product> Get()
        {
            return context.Product.ToList();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public HttpResponseMessage Post(WatchLists WatchLists)
        {
            context.WatchLists.Add(WatchLists);
            context.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created, WatchLists.Id);
        }     

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
