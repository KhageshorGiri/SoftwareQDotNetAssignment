using BookService.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookService.Infrastructure.Data;

public class BookServiceDbContext : IdentityDbContext<ApplicationUser>
{
    public BookServiceDbContext(DbContextOptions<BookServiceDbContext> options)
       : base(options)
    {
    }

    #region DBSET

    public DbSet<Book> Books { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    #endregion
}
