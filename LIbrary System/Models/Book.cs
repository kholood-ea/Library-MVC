using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LIbrary_System.Models
{
    public class Book
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }
        [DataMember]
        [Required]

        public string Auther { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid Number")]
        [DataMember]
        [Required]

        public int Copies { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid Number")]
        [DataMember]
        [Required]

        public int MaxCop { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book_Borrower> Book_Borrower { get; set; }
    }
}