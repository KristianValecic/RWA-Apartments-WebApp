using MVC_Apartmani.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVC_Apartmani.Controllers
{
    public class ApartmentController : ApiController
    {
        // GET: api/Apart
        public IHttpActionResult Get()
        {
            return Json(Repo.LoadAllApartments());
        }

        // GET: api/Apart/5
        public IHttpActionResult Get(int id)
        {
            return Json(Repo.GetApartmentById(id));
        }

        // POST: api/Apart
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Apart/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Apart/5
        public void Delete(int id)
        {
        }
    }
}
