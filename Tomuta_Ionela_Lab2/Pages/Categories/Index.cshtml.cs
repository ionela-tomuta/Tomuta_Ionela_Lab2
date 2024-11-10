using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tomuta_Ionela_Lab2.Data;
using Tomuta_Ionela_Lab2.Models;

namespace Tomuta_Ionela_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Tomuta_Ionela_Lab2.Data.Tomuta_Ionela_Lab2Context _context;

        public IndexModel(Tomuta_Ionela_Lab2.Data.Tomuta_Ionela_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!;
        public IList<Book> Books { get; set; } = default!;

        public async Task OnGetAsync(int? categoryId)
        {
            Category = await _context.Category.ToListAsync();

            if (categoryId != null)
            {
                Books = await _context.Books
                    .Include(b => b.Author)
                    .Where(b => b.BookCategories.Any(bc => bc.CategoryID == categoryId)) // Aici verificăm legătura
                    .ToListAsync();
            }
            else
            {
                Books = new List<Book>();
            }
        }
    }
}
