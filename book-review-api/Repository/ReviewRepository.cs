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
        public async Task<bool> CreateReview(Review review)
        {
            _context.Add(review);
            return await SaveAsync();
        }

  

        public async Task<ICollection<Review>> GetReviewsOfABook(int pokeId)
        {

            return await _context.Reviews.Where(r => r.Book.Id == pokeId).ToListAsync();
        }

        public async Task<bool> ReviewExists(int reviewId)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == reviewId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateReview(Review review)
        {
            _context.Update(review);
            return await SaveAsync();
        }


        public async Task<bool> DeleteReview(Review review)
        {
            _context.Remove(review);
            return await SaveAsync();
        }

        public async Task<bool> DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return await SaveAsync();
        }


    }
}
