using book_review_api.Models;

namespace book_review_api.Interfaces
{
    public interface IReviewerRepository
    {
        Task<ICollection<Reviewer>> GetReviewers();
        Task<Reviewer> GetReviewer(int reviewerId);
        Task<ICollection<Review>> GetReviewsByReviewer(int reviewerId);
        Task<bool> ReviewerExists(int reviewerId);
        Task<bool> CreateReviewer(Reviewer reviewer);
        Task<bool> UpdateReviewer(Reviewer reviewer);
        Task<bool> DeleteReviewer(Reviewer reviewer);
        Task<bool> SaveAsync();
    }
}
