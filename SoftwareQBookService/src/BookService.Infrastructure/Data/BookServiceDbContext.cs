using BookService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookService.Infrastructure.Data;

public class BookServiceDbContext : DbContext
{
    public BookServiceDbContext(DbContextOptions<BookServiceDbContext> options)
       : base(options)
    {
    }

    #region DBSET

    public DbSet<Book> Books { get; set; }

    #endregion
}
