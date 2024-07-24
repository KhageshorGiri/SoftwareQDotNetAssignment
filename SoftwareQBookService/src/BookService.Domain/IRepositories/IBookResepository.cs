using BookService.Domain.Entities;
using BookService.Shared.OperaionResponse;

namespace BookService.Domain.IRepositories;

public interface IBookResepository
{
    Task<OperationResponse<IEnumerable<Book>>> GetAllBooksAsync(CancellationToken cancellationToken = default);
    Task<OperationResponse<Book>> GetBookByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<OperationResponse<Book>> AddBookAsync(Book user, CancellationToken cancellationToken = default);
    Task<OperationResponse<Book>> UpdateBookAsync(Book user, CancellationToken cancellationToken = default);
    Task<OperationResponse<Book>> DeleteBookAsync(Book user, CancellationToken cancellationToken = default);
}
