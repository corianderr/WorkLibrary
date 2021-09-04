using ControlWork7.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlWork7.Controllers
{
    public class UsersController : Controller
    {
        private MobileContext _context;
        public UsersController(MobileContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index", "Books");
            }
            return View();
        }
    }
}
