using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlWork7.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Photo { get; set; }
        public string ReleaseYear { get; set; }
        public string Description { get; set; }
        public DateTime AddingDate { get; set; }
        public string Status { get; set; }
    }
}
