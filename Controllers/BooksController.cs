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
            var categories = _context.Categories.ToList();
            var bnc = new BookCategoryViewModel { Categories = categories, Book = new Book() };
            return View(bnc);
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                if (book != null)
                {
                    book.AddingDate = DateTime.Now;
                    book.Status = "in stock";
                    _context.Books.Add(book);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Index1()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Get(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            var bnm = new BookAndEmail { Book = book, Email = String.Empty };
            return View(bnm);
        }
        [HttpPost]
        public IActionResult Get(BookAndEmail bnm)
        {
            if (_context.Users.Any(u => u.Email == bnm.Email))
            {
                var userId = _context.Users.FirstOrDefault(u => u.Email == bnm.Email).Id;
                if (_context.Journal.Where(j => j.OtputTime == null).Where(j => j.UserId == userId).ToList().Count < 3)
                {
                    _context.Books.FirstOrDefault(b => b.Id == bnm.Book.Id).Status = "given";
                    Journal journal = new Journal { UserId = userId, BookId = bnm.Book.Id, InputTime = DateTime.Now };
                    _context.Journal.Add(journal);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("Index1");
            }
            else
                return RedirectToAction("Index2");
        }
        public IActionResult Email()
        {
            return View(new User());
        }
        [HttpPost]
        public IActionResult Email(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                var books = new List<Book>();
                var user1 = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                var journal = _context.Journal.Include(j => j.Book).Include(j => j.User).Where(j => j.OtputTime == null).Where(j => j.UserId == user1.Id).ToList();
                foreach (var item in journal)
                {
                    var book = item.Book;
                    books.Add(book);
                }
                return View("PersonalArea", books);
            }
            else
                return RedirectToAction("Index2");
        }
        public IActionResult Return(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            book.Status = "in stock";
            _context.Journal.Where(j => j.BookId == bookId).FirstOrDefault(j => j.OtputTime == null).OtputTime = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int bookId)
        {
            var book = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == bookId);
            return View(book);
        }
        public IActionResult TakenBooks()
        {
            var journal = _context.Journal.Include(j => j.Book).Include(j => j.User).Where(j => j.OtputTime == null);
            return View(journal.ToList());
        }
    }
}
