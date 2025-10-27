using Bookshelf.Data;
using Bookshelf.Models;
using Bookshelf.Models.StatisticsViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Pages
{
    public class StatisticsModel : PageModel
    {
        private readonly BookshelfContext _context;
        public StatisticsModel(BookshelfContext context)
        {
            _context = context;
        }

        // create an instance of Statistics model
        public Statistics BookStatistics { get; set; }
        
        // do not need IList because we are not grouping by titles and therefore
        // we do not need IQueryable for further grouping, can just use EF core methods
        // methods are async like our other methods for accessing DB

        public async Task OnGetAsync()
        {
            int bookCount = await _context.Books.CountAsync(); // count Book entities asynchronously 
            // https://wirefuture.com/post/how-to-use-entity-framework-core-in-net-8-efficiently
            // section 1. Prefer Asynchronous Methods for I/O-bound Operations

            int authorCount = await _context.Books // get books to count distinct authors
                .Select(book => book.Author) // select the author
                // same tutorial section 2. Utilize Projection Queries to Fetch Only Required Columns
                .Distinct() // ef core method for selecting distinct for no duplicates
                            // https://learn.microsoft.com/en-us/dotnet/api/system.linq.queryable.distinct?view=net-9.0
                .CountAsync(); // ef core async from section 1

            int pagesCount = await _context.Books // get books to sum pages
                .Select(book => book.Pages) // select pages
                .SumAsync(); // sum them

            // create new and then update BookStatistics with DB SQL
            BookStatistics = new Statistics();
            BookStatistics.BookCount = bookCount;
            BookStatistics.AuthorCount = authorCount;
            BookStatistics.PagesCount = pagesCount;
        }

    }
}
