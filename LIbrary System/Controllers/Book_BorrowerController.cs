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
    public class Book_BorrowerController : ApiController
    {
        private LibrarySystemDBcontext db = new LibrarySystemDBcontext();

        //GET: api/Book_Borrower
        public IQueryable<Book_Borrower> GetBook_Borrower()
        {
            return db.Book_Borrower;
        }


        //GET: api/Book_Borrower/5
        [ResponseType(typeof(Book_Borrower))]
        [HttpGet]
        //public IHttpActionResult GetBook_BorrowerFromAnotherFunction(int id1, int id2)
        //{
        //    return Ok("hi from my method");
        //    //return db.Book_Borrower.Where(e => e.BookId==id1);
        //}
        public IHttpActionResult GetBook_Borrower(int id1, int id2)
        {
            Book_Borrower book_Borrower = db.Book_Borrower.Find(id1,id2);

            //Book_Borrower book_Borrower = db.Book_Borrower.Find(id);
            //Book_Borrower book_Borrower = new Book_Borrower();
            if (book_Borrower == null)
            {
                return NotFound();
            }
            if (book_Borrower.BookId == id1 && book_Borrower.BorrowerId == id2)
            {
                return Ok(book_Borrower);
            }
            return Ok(book_Borrower);
        }

        // PUT: api/Book_Borrower/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook_Borrower(int id1,int id2, Book_Borrower book_Borrower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id1 != book_Borrower.BookId && id2!=book_Borrower.BorrowerId)
            {
                return BadRequest();
            }

            db.Entry(book_Borrower).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Book_BorrowerExists(id1,id2))
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

        // POST: api/Book_Borrower
        [ResponseType(typeof(Book_Borrower))]
        public IHttpActionResult PostBook_Borrower(Book_Borrower book_Borrower)
        {
            IList<Book> books = db.Book.ToList();
            Book book = new Book();
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Id==book_Borrower.BookId)
                {
                    book = books[i];
                }

            }
            if (book.Copies == 0)
            {
                return BadRequest("Book is out of Stock");

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Book_Borrower.Add(book_Borrower);
            book.Copies--;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Book_BorrowerExists(book_Borrower.BookId,book_Borrower.BorrowerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book_Borrower.BookId }, book_Borrower);
        }

        // DELETE: api/Book_Borrower/5
        [ResponseType(typeof(Book_Borrower))]
        public IHttpActionResult DeleteBook_Borrower(int id1,int id2)
        {
            Book_Borrower book_Borrower = db.Book_Borrower.Find(id1,id2);
            Book book = db.Book.Find(book_Borrower.BookId);
            if (book_Borrower == null)
            {
                return NotFound();
            }
            if (book_Borrower.ReturnedDate!=null)
            {
            book.Copies++;
            }
            db.Book_Borrower.Remove(book_Borrower);
            db.SaveChanges();


            return Ok(book_Borrower);
        }
        [ResponseType(typeof(Book_Borrower))]
        [HttpGet] 
        public IHttpActionResult returnBook(int id1,int id2)
        {
            Book_Borrower book_Borrower = db.Book_Borrower.Find(id1, id2);
            Book book = db.Book.Find(id1);
            
            //IList<Book_Borrower> book_Borrower = db.Book_Borrower.ToList();
            //for (int i = 0; i < book_Borrower.Count; i++)
            //{



                if (book_Borrower.ReturnedDate != null)
                {

                    return Ok("Book is already Returned");

                }
                if (  book_Borrower.ReturnedDate == null )
                {
                    book_Borrower.ReturnedDate = DateTime.Now;
                    book.Copies++;
                    db.SaveChanges();
                    return Ok("Book is Returned");

                }
                if (book.Copies >= book.MaxCop)
                {

                    return Ok("Maximum Copies of book on shelf Exceeded");

                }

                else return Ok("Book is not Returned yet");
            
            //db.SaveChanges();
            //return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool Book_BorrowerExists(int id)
        //{
        //    return db.Book_Borrower.Count(e => e.BookId == id) > 0;
        //}
        private bool Book_BorrowerExists(int id1 ,int id2)
        {
            return db.Book_Borrower.Count(e => e.BookId == id1&&e.BorrowerId==id2) > 0;
        }
    }
}