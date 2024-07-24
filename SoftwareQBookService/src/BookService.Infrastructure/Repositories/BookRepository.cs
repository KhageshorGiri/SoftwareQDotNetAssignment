using BookService.Domain.Entities;
using BookService.Domain.IRepositories;
using BookService.Infrastructure.Data;
using BookService.Shared.OperaionResponse;
using Microsoft.EntityFrameworkCore;

namespace BookService.Infrastructure.Repositories;

public class BookRepository : IBookResepository
{
    private readonly BookServiceDbContext _dbContext;
    public BookRepository(BookServiceDbContext context)
    {
        _dbContext = context;
    }

    public async Task<OperationResponse<IEnumerable<Book>>> GetAllBooksAsync(CancellationToken cancellationToken = default)
    {
        var queryResult = await _dbContext.Books.AsQueryable()
            .AsNoTracking()
            .ToListAsync();

        return OperationResponse<IEnumerable<Book>>.Success("Success", queryResult);
    }

    public async Task<OperationResponse<Book>> GetBookByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var queryResult = await _dbContext.Books.Where(x => x.Id == id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);

        return OperationResponse<Book>.Success("Success", queryResult);
    }

    public async Task<OperationResponse<Book>> AddBookAsync(Book newBook, CancellationToken cancellationToken = default)
    {
        var response = new OperationResponse<Book>();
        using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                await _dbContext.Books.AddAsync(newBook);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync(cancellationToken);
                return OperationResponse<Book>.Success("New Book added successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return OperationResponse<Book>.Failure("An error occurred while adding the new book");
            }
        }
    }

    public async Task<OperationResponse<Book>> UpdateBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                _dbContext.Books.Update(book);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return OperationResponse<Book>.Success("Book updated successfully", book);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return OperationResponse<Book>.Failure("An error occurred while updating the book");
            }
        }
    }

    public async Task<OperationResponse<Book>> DeleteBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return OperationResponse<Book>.Success("Book Deleted successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return OperationResponse<Book>.Failure("An error occurred while deleting the book");
            }
        }
    }
}
