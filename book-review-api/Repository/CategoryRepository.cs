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

        public async Task<bool> CreateCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            return await SaveAsync();
        }
        public async Task<ICollection<Book>> GetBookByCategory(int categoryId)
        {
           return await _context.BookCategories.Where(e => e.BookId == categoryId).Select(x => x.Book).ToListAsync();
        }

     

        public async Task<Category> GetCategory(int id)
        {
          return await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
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
