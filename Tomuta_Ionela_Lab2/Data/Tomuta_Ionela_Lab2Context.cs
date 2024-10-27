using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tomuta_Ionela_Lab2.Models;

namespace Tomuta_Ionela_Lab2.Data
{
    public class Tomuta_Ionela_Lab2Context : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public Tomuta_Ionela_Lab2Context (DbContextOptions<Tomuta_Ionela_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Tomuta_Ionela_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Tomuta_Ionela_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Tomuta_Ionela_Lab2.Models.Category> Category { get; set; } = default!;
    }
}
