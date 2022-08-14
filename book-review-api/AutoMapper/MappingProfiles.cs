using AutoMapper;
using book_review_api.Dto;
using book_review_api.Dto.ReviewDto;
using book_review_api.Dto.Reviewer;
using book_review_api.Models;

namespace book_review_api.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BookCreateDto, Book>();
            CreateMap<Book, BookDto>();

            CreateMap<ReviewCreateDto, Review>();
            CreateMap<Review, ReviewDto>();

            CreateMap<ReviewerCreateDto, Reviewer>();
            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
