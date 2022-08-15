using book_review_api.Models;

namespace book_review_api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<ICollection<Book>> GetBookByCategory(int categoryId);
        Task<bool> CategoryExists(int id);
        Task<bool> CreateCategory(Category category);
        Task<bool> UpdateCategory(Category category);
        Task<bool> DeleteCategory(Category category);
        Task<bool> SaveAsync();
    }
}
