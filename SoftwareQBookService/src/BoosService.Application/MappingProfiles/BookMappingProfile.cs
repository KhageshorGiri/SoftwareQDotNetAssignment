using AutoMapper;
using BookService.Domain.Entities;
using BoosService.Application.Dtos;

namespace BoosService.Application.MappingProfiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<CreateBookDto, Book>();
        CreateMap<Book, BookListDto>();
        CreateMap<UpdateBookDto, Book>();
    }
}
