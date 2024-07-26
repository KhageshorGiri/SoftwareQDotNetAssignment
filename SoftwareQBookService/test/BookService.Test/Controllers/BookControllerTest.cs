using BookService.API.Controllers;
using BookService.Application.Dtos;
using BookService.Application.IBusinesses;
using BookService.Shared.Enums;
using BookService.Shared.OperaionResponse;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookService.Test.Controllers;

public class BooksControllerTest
{
    private readonly Random rand = new Random();
    private readonly Mock<IBookBusiness> _mockBookService;
    private readonly BooksController _controller;

    public BooksControllerTest()
    {
        _mockBookService = new Mock<IBookBusiness>();
        _controller = new BooksController(_mockBookService.Object);
    }


    [Fact]
    public async Task GetBookById_ReturnsBook_WhenBookExists()
    {
        // Arrange
        var bookId = 1;
        var bookDto = new BookListDto { Id = bookId, Title = "Test Book", Author = "Ram", Genre="Novel", PublicationYear=2023};
        var expectedResponse = OperationResponse<BookListDto>.Success("Book found.", bookDto);

        _mockBookService.Setup(service => service.GetBookByIdAsync(bookId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetBookById(bookId);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Equal(ResponseTypeOption.Success, result.ResponseType);
        Assert.IsType<OperationResponse<BookListDto>>(result);
        Assert.IsType<IEnumerable<BookListDto>>(result.Data);
    }

    [Fact]
    public async Task GetBookById_ReturnsNotFound_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = 1;
        var expectedResponse = OperationResponse<BookListDto>.Failure("Book not found.");

        _mockBookService.Setup(service => service.GetBookByIdAsync(bookId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetBookById(bookId);

        // Assert
        Assert.NotNull(result.Message);
        Assert.Null(result.Data);
        Assert.Equal(ResponseTypeOption.Failed, result.ResponseType);
        Assert.IsType<OperationResponse<BookListDto>>(result);
    }

    [Fact]
    public async Task Post_WithBookToAdd_ReturnOperationResponseSucces()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var bookDto = new CreateBookDto {Title = "Test Book", Author = "Ram", Genre = "Novel", PublicationYear = 2023 };
        var expectedResponse = OperationResponse<CreateBookDto>.Success("Book created successfully.", bookDto);

        _mockBookService.Setup(service => service.AddBookAsync(bookDto, cancellationToken))
            .ReturnsAsync(expectedResponse);

        // Act
        var actionResult = await _controller.Post(bookDto, cancellationToken);

        // Assert
        Assert.Null(actionResult.Data);
        Assert.Equal(ResponseTypeOption.Success, actionResult.ResponseType);
        Assert.IsType<OperationResponse>(actionResult);
    }

    [Fact]
    public async Task Post_WithInvalidBook_ReturnsOperationResponseFailed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var bookDto = new CreateBookDto {Title = "", Author = "Ram", Genre = "Novel", PublicationYear = 2023 };

        _controller.ModelState.AddModelError("Title", "Title is required");

        var expectedResponse = OperationResponse<CreateBookDto>.Failure("Failed");

        _mockBookService.Setup(service => service.AddBookAsync(bookDto, cancellationToken))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.Post(bookDto, cancellationToken);

        // Assert
        Assert.Equal(ResponseTypeOption.Failed, result.ResponseType);
        Assert.NotEmpty(result.Message);
        Assert.NotNull(result.Message);
    }

}
