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
    public class loginsController : ApiController
    {
        private restEntities db = new restEntities();

        // GET: api/logins
        public IQueryable<login> Getlogins()
        {
            return db.logins;
        }

        // GET: api/logins/5
        [ResponseType(typeof(login))]
        public IHttpActionResult Getlogin(string id)
        {
            login login = db.logins.Find(id);
            if (login == null)
            {
                return NotFound();
            }

            return Ok(login);
        }

        // PUT: api/logins/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlogin(string id, login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != login.username)
            {
                return BadRequest();
            }

            db.Entry(login).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!loginExists(id))
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

        // POST: api/logins
        [ResponseType(typeof(login))]
        public IHttpActionResult Postlogin(login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.logins.Add(login);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (loginExists(login.username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = login.username }, login);
        }

        // DELETE: api/logins/5
        [ResponseType(typeof(login))]
        public IHttpActionResult Deletelogin(string id)
        {
            login login = db.logins.Find(id);
            if (login == null)
            {
                return NotFound();
            }

            db.logins.Remove(login);
            db.SaveChanges();

            return Ok(login);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool loginExists(string id)
        {
            return db.logins.Count(e => e.username == id) > 0;
        }
    }
}