using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlWork7.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public string ReleaseYear { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime AddingDate { get; set; }
        public string Status { get; set; }
    }
}
