﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlWork7.Models
{
    public class MobileContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public MobileContext(DbContextOptions<MobileContext> options) : base(options) { }
    }
}