using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LIbrary_System.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        [Unique]
        public string Email { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]

        public virtual ICollection<Book_Borrower> Book_Borrower { get; set; }
    }
}