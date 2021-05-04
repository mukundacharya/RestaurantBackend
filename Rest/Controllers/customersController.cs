using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Rest.Models;

namespace Rest.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class customersController : ApiController
    {
        private restEntities db = new restEntities();

        // GET: api/customers
        public IQueryable<customerInfo> GetcustomerInfoes()
        {
            return db.customerInfoes;
        }

        // GET: api/customers/5
        [ResponseType(typeof(customerInfo))]
        public IHttpActionResult GetcustomerInfo(int id)
        {
            customerInfo customerInfo = db.customerInfoes.Find(id);
            if (customerInfo == null)
            {
                return NotFound();
            }

            return Ok(customerInfo);
        }

        // PUT: api/customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutcustomerInfo(int id, customerInfo customerInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerInfo.customerID)
            {
                return BadRequest();
            }

            db.Entry(customerInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customerInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/customers
        [ResponseType(typeof(customerInfo))]
        public IHttpActionResult PostcustomerInfo(customerInfo customerInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.customerInfoes.Add(customerInfo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customerInfo.customerID }, customerInfo);
        }

        // DELETE: api/customers/5
        [ResponseType(typeof(customerInfo))]
        public IHttpActionResult DeletecustomerInfo(int id)
        {
            customerInfo customerInfo = db.customerInfoes.Find(id);
            if (customerInfo == null)
            {
                return NotFound();
            }

            db.customerInfoes.Remove(customerInfo);
            db.SaveChanges();

            return Ok(customerInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool customerInfoExists(int id)
        {
            return db.customerInfoes.Count(e => e.customerID == id) > 0;
        }
    }
}