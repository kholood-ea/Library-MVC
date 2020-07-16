using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LIbrary_System.Models;

namespace LIbrary_System.Controllers
{
    public class PersonsController : ApiController
    {
        private LibrarySystemDBcontext db = new LibrarySystemDBcontext();

        // GET: api/Persons
        public IQueryable<Person> GetPerson()
        {
            return db.Person;
        }

        // GET: api/Persons/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPerson(int id)
        {
            Person Person = db.Person.Find(id);
            if (Person == null)
            {
                return NotFound();
            }

            return Ok(Person);
        }

        //Search :api/Persons/search/search?search_word=abc
        [HttpGet]

        public IQueryable<Person> Search(string search_word)
        {
            //return db.Person.Where(e=>e.Email.Contains(search_word));
            return db.Person.Where(e => e.Email==(search_word));

        }
        // PUT: api/Persons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerson(int id, Person Person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Person.Id)
            {
                return BadRequest();
            }

            db.Entry(Person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/Persons
        [ResponseType(typeof(Person))]
        public IHttpActionResult PostPerson(Person Person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Person.Add(Person);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Person.Id }, Person);
        }

        // DELETE: api/Persons/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(int id)
        {
            Person Person = db.Person.Find(id);
            if (Person == null)
            {
                return NotFound();
            }

            db.Person.Remove(Person);
            db.SaveChanges();

            return Ok(Person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.Person.Count(e => e.Id == id) > 0;
        }
    }
}