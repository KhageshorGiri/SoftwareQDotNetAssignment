using BookService.Shared.OperaionResponse;
using BookService.Application.Dtos;

namespace BookService.Application.IBusinesses;

public interface IBookBusiness
{
    Task<OperationResponse<IEnumerable<BookListDto>>> GetAllBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<OperationResponse<BookListDto>> GetBookByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<OperationResponse<CreateBookDto>> AddBookAsync(CreateBookDto newBook, CancellationToken cancellationToken = default);
    Task<OperationResponse<UpdateBookDto>> UpdateBookAsync(int id, UpdateBookDto bookTOUpdate, CancellationToken cancellationToken = default);
    Task<OperationResponse> DeleteBookAsync(int id, CancellationToken cancellationToken = default);
}
