using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bookshelf.Data;
using Bookshelf.Models;

namespace Bookshelf.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Bookshelf.Data.BookshelfContext _context;

        public CreateModel(Bookshelf.Data.BookshelfContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyBook = new Book();

            if (await TryUpdateModelAsync<Book>(
                emptyBook,
                "book",   // Prefix for form value.
                b => b.Title, b => b.Author, b => b.Genre, b => b.Rating, b => b.Pages))
            {
                _context.Books.Add(emptyBook);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
