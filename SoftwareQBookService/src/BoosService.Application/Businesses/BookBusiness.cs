using AutoMapper;
using BookService.Domain.Entities;
using BookService.Domain.IRepositories;
using BookService.Shared.Enums;
using BookService.Shared.OperaionResponse;
using BookService.Application.Dtos;
using BookService.Application.IBusinesses;

namespace BookService.Application.Businesses;

public class BookBusiness : IBookBusiness
{
    private readonly IBookResepository _bookRepository;
    private readonly IMapper _mapper;
    public BookBusiness(IBookResepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }
    public async Task<OperationResponse<IEnumerable<BookListDto>>> GetAllBooksAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var respone = new OperationResponse<IEnumerable<BookListDto>>();

        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 10) pageSize = 10;

        var allBooks = await _bookRepository.GetAllBooksAsync(pageNumber, pageSize, cancellationToken);

        if (allBooks.Data is null)
            return OperationResponse<IEnumerable<BookListDto>>.Failure($"No Data Found");

        var result = _mapper.Map<IEnumerable<BookListDto>>(allBooks.Data);
        respone.ResponseType = ResponseTypeOption.Success;
        respone.Message = "Data Fetched Successfully";
        respone.Data = result;
        return respone;
    }
    public async Task<OperationResponse<BookListDto>> GetBookByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var respone = new OperationResponse<BookListDto>();
        var book = await _bookRepository.GetBookByIdAsync(id, cancellationToken);

        if (book.Data is null)
            return OperationResponse<BookListDto>.Failure($"Book with id {id} is not found.");

        var result = _mapper.Map<BookListDto>(book.Data);
        respone.ResponseType = ResponseTypeOption.Success;
        respone.Message = "Data Fetched Successfully";
        respone.Data = result;
        return respone;
    }
    public async Task<OperationResponse<CreateBookDto>> AddBookAsync(CreateBookDto newBook, CancellationToken cancellationToken = default)
    {
        var respone = new OperationResponse<CreateBookDto>();
        var mappedBook = _mapper.Map<Book>(newBook);

        mappedBook.CreatedBy = 1; // TODO: Change by real user id
        mappedBook.CreatedOn = DateTime.UtcNow;

        var result = await _bookRepository.AddBookAsync(mappedBook, cancellationToken);

        if (result.ResponseType != ResponseTypeOption.Success)
            return OperationResponse<CreateBookDto>.Failure(result.Message);

        respone.ResponseType = ResponseTypeOption.Success;
        respone.Message = "New Book Added Successfully";
        return respone;
    }
    public async Task<OperationResponse<UpdateBookDto>> UpdateBookAsync(int id, UpdateBookDto bookToUpdate, CancellationToken cancellationToken = default)
    {
        var existingBookData = await _bookRepository.GetBookByIdAsync(id, cancellationToken);

        if (existingBookData.Data is null)
            return OperationResponse<UpdateBookDto>.Failure($"Book with id {id} is not found.");

        var existingBookUpdatedValue = _mapper.Map(bookToUpdate, existingBookData.Data);
        existingBookUpdatedValue.LastUpdatedBy = 1; // TODO: Change by real user id
        existingBookUpdatedValue.LastUpdatedOn = DateTime.UtcNow;

        var result = await _bookRepository.UpdateBookAsync(existingBookUpdatedValue, cancellationToken);

        if (result.ResponseType != ResponseTypeOption.Success)
            return OperationResponse<UpdateBookDto>.Failure(result.Message);

        return OperationResponse<UpdateBookDto>.Success($"Book With id {id} updated succefully.");
    }

    public async Task<OperationResponse> DeleteBookAsync(int id, CancellationToken cancellationToken = default)
    {
        var bookToDelete = await _bookRepository.GetBookByIdAsync(id, cancellationToken);

        if (bookToDelete.Data is null)
            return OperationResponse.Failure($"Book with id {id} is not found.");

        var updatedBook = bookToDelete.Data;

        var result = await _bookRepository.DeleteBookAsync(updatedBook);

        if (result.ResponseType != ResponseTypeOption.Success)
            return OperationResponse.Failure($"Book with id {id} is can not not deleted.");

        return OperationResponse.Success("Bokk Deleted Successfully");
    }

}
