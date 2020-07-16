using LIbrary_System.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace LIbrary_System.Controllers
{
    public class BooksController : ApiController
    {
        private LibrarySystemDBcontext db = new LibrarySystemDBcontext();

        
        public IQueryable<Book> GetBook()
        {
            return db.Book;
        }

        //GET: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);

        }
        //Search :api/books/search/search?search_word=complete
        [HttpGet]
       
        public IQueryable<Book> Search(string search_word)
        {
            return db.Book.Where(e => e.Name .Contains(search_word));
        }
   

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public  IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }
            if (book.Copies>book.MaxCop)
            {
                return BadRequest("Book is out of Stock");
            }
            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public  IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (book.Copies > book.MaxCop && book!=null)
            {
                return BadRequest("Book is out of Stock");
            }

            db.Book.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Book.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Book.Count(e => e.Id == id) > 0;
        }
    }
}