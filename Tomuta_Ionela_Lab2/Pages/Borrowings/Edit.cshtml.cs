using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tomuta_Ionela_Lab2.Data;
using Tomuta_Ionela_Lab2.Models;

namespace Tomuta_Ionela_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Tomuta_Ionela_Lab2.Data.Tomuta_Ionela_Lab2Context _context;

        public EditModel(Tomuta_Ionela_Lab2.Data.Tomuta_Ionela_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing
                .Include(b => b.Book)
                .ThenInclude(b => b.Author)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (borrowing == null)
            {
                return NotFound();
            }

            Borrowing = borrowing;

            // Populate the dropdown list for BookID with detailed information.
            var bookList = _context.Books
                .Include(b => b.Author)
                .Select(b => new
                {
                    b.ID,
                    BookFullName = b.Title + " - " + b.Author.LastName + " " + b.Author.FirstName
                });
            ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName");

            // Populate the dropdown list for MemberID with full member names.
            ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");

            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(Borrowing.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.ID == id);
        }
    }
}