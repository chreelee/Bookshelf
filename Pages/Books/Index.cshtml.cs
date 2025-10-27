using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Data;
using Bookshelf.Models;

namespace Bookshelf.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Bookshelf.Data.BookshelfContext _context;
        public IndexModel(Bookshelf.Data.BookshelfContext context)
        {
            _context = context;
        }

        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string GenreSort { get; set; }
        public string RatingSort { get; set; } // sorts numbers
        public string PagesSort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Book> Books { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : ""; // default 
            RatingSort = sortOrder == "Rating" ? "rating_desc" : "Rating";
            AuthorSort = sortOrder == "Author" ? "author_desc" : "Author";
            GenreSort = sortOrder == "Genre" ? "genre_desc" : "Genre";
            PagesSort = sortOrder == "Pages" ? "pages_desc" : "Pages";

            CurrentFilter = searchString; // add filtering

            IQueryable<Book> booksIQ = from b in _context.Books
                                       select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                booksIQ = booksIQ.Where(s => s.Title.Contains(searchString) || s.Author.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    booksIQ = booksIQ.OrderByDescending(b => b.Title);
                    break;
                case "Author":
                    booksIQ = booksIQ.OrderBy(b => b.Author);
                    break;
                case "author_desc":
                    booksIQ = booksIQ.OrderByDescending(b => b.Author);
                    break;
                case "Genre":
                    booksIQ = booksIQ.OrderBy(b => b.Genre);
                    break;
                case "genre_desc":
                    booksIQ = booksIQ.OrderByDescending(b => b.Genre);
                    break;
                case "Rating":
                    booksIQ = booksIQ.OrderBy(b => b.Rating);
                    break;
                case "rating_desc":
                    booksIQ = booksIQ.OrderByDescending(b => b.Rating);
                    break;
                case "Pages":
                    booksIQ = booksIQ.OrderBy(b => b.Pages);
                    break;
                case "pages_desc":
                    booksIQ = booksIQ.OrderByDescending(b => b.Pages);
                    break;
                default: booksIQ = booksIQ.OrderBy(b => b.Title);
                    break;
            }

            Books = await booksIQ.AsNoTracking().ToListAsync();
        }
    }
}
