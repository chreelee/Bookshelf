using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Models;

namespace Bookshelf.Data
{
    public class BookshelfContext : DbContext
    {
        public BookshelfContext (DbContextOptions<BookshelfContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; } // https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-9.0&tabs=visual-studio#update-the-database-context-class

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books"); 
        }
    }
}
