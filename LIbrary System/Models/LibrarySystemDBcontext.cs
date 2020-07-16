using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LIbrary_System.Models
{
    public class LibrarySystemDBcontext:DbContext
    {
        public DbSet<Book> Book { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Book_Borrower> Book_Borrower { get; set; }

    }
}