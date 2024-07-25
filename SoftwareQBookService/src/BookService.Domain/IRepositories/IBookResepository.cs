using BookService.Domain.Entities;
using BookService.Shared.OperaionResponse;

namespace BookService.Domain.IRepositories;

public interface IBookResepository
{
    Task<OperationResponse<IEnumerable<Book>>> GetAllBooksAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<OperationResponse<Book>> GetBookByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<OperationResponse<Book>> AddBookAsync(Book newBook, CancellationToken cancellationToken = default);
    Task<OperationResponse<Book>> UpdateBookAsync(Book bookToUpdate, CancellationToken cancellationToken = default);
    Task<OperationResponse<Book>> DeleteBookAsync(Book bookToDelete, CancellationToken cancellationToken = default);
}
