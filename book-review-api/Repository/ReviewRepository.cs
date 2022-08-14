using AutoMapper;
using book_review_api.Data;
using book_review_api.Interfaces;
using book_review_api.Models;
using Microsoft.EntityFrameworkCore;

namespace book_review_api.Repository
{
    public class ReviewRepository : IReviewRepository
    {


        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ICollection<Review>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetReview(int reviewId)
        {
            return await _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefaultAsync();
        }
        public Task<bool> CreateReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteReviews(List<Review> reviews)
        {
            throw new NotImplementedException();
        }

      

        public async Task<ICollection<Review>> GetReviewsOfABook(int pokeId)
        {

            return await _context.Reviews.Where(r => r.Book.Id == pokeId).ToListAsync();
        }

        public async Task<bool> ReviewExists(int reviewId)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == reviewId);
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
