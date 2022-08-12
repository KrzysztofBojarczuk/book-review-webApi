using AutoMapper;
using book_review_api.Dto;
using book_review_api.Models;

namespace book_review_api.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookCreateDto, Book>();
        }
    }
}
