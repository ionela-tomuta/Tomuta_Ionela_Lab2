using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tomuta_Ionela_Lab2.Data;
using Tomuta_Ionela_Lab2.Models;

namespace Tomuta_Ionela_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Tomuta_Ionela_Lab2.Data.Tomuta_Ionela_Lab2Context _context;

        public IndexModel(Tomuta_Ionela_Lab2.Data.Tomuta_Ionela_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;
        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }

        public string CurrentFilter { get; set; }
        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            BookD = new BookData();

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";

            CurrentFilter = searchString;

            // Inițializăm lista cu toate cărțile
            var books = _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .Include(b => b.Author)
                .AsNoTracking();

            // Aplicăm filtrul de căutare
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s =>
                    s.Author.FirstName.Contains(searchString) ||
                    s.Author.LastName.Contains(searchString) ||
                    s.Title.Contains(searchString));
            }

            // Aplicăm sortarea
            books = sortOrder switch
            {
                "title_desc" => books.OrderByDescending(s => s.Title),
                "author_desc" => books.OrderByDescending(s => s.Author.LastName), // FullName nu poate fi folosit direct
                "author" => books.OrderBy(s => s.Author.LastName),
                _ => books.OrderBy(s => s.Title),
            };

            BookD.Books = await books.ToListAsync();

            // Setăm BookID pentru cărțile filtrate
            if (id != null)
            {
                BookID = id.Value;
                Book book = BookD.Books
                    .FirstOrDefault(i => i.ID == id.Value);

                if (book != null)
                {
                    BookD.Categories = book.BookCategories.Select(s => s.Category).ToList();
                }
            }
        }

    }
}