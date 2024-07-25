using BookService.Application.Dtos;
using BookService.Shared.OperaionResponse;
using BookService.Application.IBusinesses;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Microsoft.AspNetCore.Authorization;

namespace BookService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookBusiness _bookService;
    public BooksController(IBookBusiness bookService)
    {
        _bookService = bookService;
    }

    // Get: api/books
    [HttpGet]
    public async Task<OperationResponse<IEnumerable<BookListDto>>> GetAllBooks(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await _bookService.GetAllBooksAsync(pageNumber, pageSize, cancellationToken);
        return OperationResponse<IEnumerable<BookListDto>>.Success(result.Message, result.Data);
    }

    // GET: api/books/{id}
    [HttpGet("{id}")]
    public async Task<OperationResponse<BookListDto>> GetBookById(int id, CancellationToken cancellationToken = default)
    {
        var result = await _bookService.GetBookByIdAsync(id, cancellationToken);
        return OperationResponse<BookListDto>.Success(result.Message, result.Data);
    }

    // POST: api/books
    [HttpPost]
    public async Task<OperationResponse> Post([FromBody] CreateBookDto newBook, CancellationToken cancellationToken = default)
    {
        var result = await _bookService.AddBookAsync(newBook, cancellationToken);
        return OperationResponse.Success(result.Message);
    }

    // PUT: api/books/[id}
    [HttpPut("{id}")]
    public async Task<OperationResponse> Put(int id, [FromBody] UpdateBookDto book, CancellationToken cancellationToken = default)
    {
        var result = await _bookService.UpdateBookAsync(id, book, cancellationToken);
        return OperationResponse.Success(result.Message);
    }

    // DELETE: api/books/{id}
    [HttpDelete("{id}")]
    public async Task<OperationResponse> Delete(int id, CancellationToken cancellationToken = default)
    {
        var result = await _bookService.DeleteBookAsync(id, cancellationToken);
        return OperationResponse.Success(result.Message);
    }
}
