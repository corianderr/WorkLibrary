using ControlWork7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlWork7.ViewModels
{
    public class BookCategoryViewModel
    {
        public Book? Book { get; set; }
        public List<Category> Categories { get; set; }
    }
}
