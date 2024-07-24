using AutoMapper;
using BookService.Domain.Entities;
using BookService.Application.Dtos;

namespace BookService.Application.MappingProfiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<CreateBookDto, Book>();
        CreateMap<Book, BookListDto>();
        CreateMap<UpdateBookDto, Book>();
    }
}
