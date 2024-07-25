using BookService.API.Controllers;
using BookService.Application.Dtos;
using BookService.Application.IBusinesses;
using BookService.Shared.Enums;
using BookService.Shared.OperaionResponse;
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
        var result = await _controller.GetBookById(bookId) as OperationResponse;

        // Assert
        Assert.NotNull(result.Data);
        Assert.Equal(ResponseTypeOption.Success, result.ResponseType);
        Assert.IsType<OperationResponse<BookListDto>>(result.Data);
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
        var result = await _controller.GetBookById(bookId) as OperationResponse;

        // Assert
        Assert.NotNull(result.Message);
        Assert.Equal(ResponseTypeOption.Failed, result.ResponseType);
        Assert.Null(result.Data);
    }

}
