using ControlWork7.Models;
using ControlWork7.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 8;
            IQueryable<Book> source = _context.Books.OrderByDescending(s => s.AddingDate);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Books = items
            };
            return View(viewModel);
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
