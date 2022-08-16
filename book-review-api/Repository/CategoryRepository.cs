using book_review_api.Data;
using book_review_api.Interfaces;
using book_review_api.Models;
using Microsoft.EntityFrameworkCore;

namespace book_review_api.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public Task<bool> CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }
        public Task<ICollection<Book>> GetBookByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

     

        public Task<Category> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            _context.Update(category);
            return await SaveAsync();
        }
        public async Task<bool> DeleteCategory(Category category)
        {
            _context.Remove(category);
            return await SaveAsync();
        }
    }
}
