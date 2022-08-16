using AutoMapper;
using book_review_api.Data;
using book_review_api.Interfaces;
using book_review_api.Models;
using Microsoft.EntityFrameworkCore;

namespace book_review_api.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<Reviewer>> GetReviewers()
        {
            return await _context.Reviewers.ToListAsync();
        }
        public Task<bool> CreateReviewer(Reviewer reviewer)
        {
            throw new NotImplementedException();
        }

       

        public async Task<Reviewer> GetReviewer(int reviewerId)
        {
            return await _context.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefaultAsync();
        }


        public Task<ICollection<Review>> GetReviewsByReviewer(int reviewerId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ReviewerExists(int reviewerId)
        {
            return await _context.Reviewers.AnyAsync(r => r.Id == reviewerId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public Task<bool> UpdateReviewer(Reviewer reviewer)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return await SaveAsync();
        }
    }
}
