using book_review_api.Models;

namespace book_review_api.Interfaces
{
    public interface IReviewRepository
    {

        Task<ICollection<Review>> GetReviews();
        Task<Review> GetReview(int reviewId);
        Task<ICollection<Review>> GetReviewsOfABook(int pokeId);
        Task<bool> ReviewExists(int reviewId);
        Task<bool> CreateReview(Review review);
        Task<bool> UpdateReview(Review review);
        Task<bool> DeleteReview(Review review);
        Task<bool> DeleteReviews(List<Review> reviews);
        Task<bool> SaveAsync();
    }
}
