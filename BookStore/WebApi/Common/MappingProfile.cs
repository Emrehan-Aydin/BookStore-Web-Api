using AutoMapper;
using WebApi.Application.BookOperations.Queries.GetByIdQuery;
using WebApi.Application.GerneOperation.Queries.GetGenreDetails;
using WebApi.Application.GerneOperation.Queries.GetGenres;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.CreateBookCommand.CreateBookCommand;
using static WebApi.Application.BookOperations.Queries.GetBooks.GetBooksQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));       
            CreateMap<Book, BookModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));   
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
        }
        
    }
}