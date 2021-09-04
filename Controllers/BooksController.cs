using ControlWork7.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlWork7.Controllers
{
    public class BooksController : Controller
    {
        private MobileContext _context;

        public BooksController(MobileContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IQueryable<Book> books = _context.Books;
            books = books.OrderByDescending(s => s.AddingDate);
            return View(books.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (book != null)
            {
                book.AddingDate = DateTime.Now;
                book.Status = "new";
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
