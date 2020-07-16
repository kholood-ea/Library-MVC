using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LIbrary_System.Models
{
    public class Book_Borrower
    {
        [Key]
        [Required]
        [Column(Order = 1)]

        public int BookId { get; set; }
        [Required]

        public string BookName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]

        public virtual Book Book { get; set; }
        [Required]
        [Key]
        [Column(Order = 2)]
        public int BorrowerId { get; set; }
        [Required]

        public string BorrowerEmail { get; set; }

        public DateTime? ReturnedDate { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Person Person { get; set; }
    }
}