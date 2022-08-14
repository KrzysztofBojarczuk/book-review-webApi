using book_review_api.Data;
using book_review_api.Interfaces;
using book_review_api.Models;
using Microsoft.EntityFrameworkCore;

namespace book_review_api.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            return await SaveAsync();
        }

        public async Task<bool> DeleteBookAsync(Book book)
        {
             _context.Remove(book);
            return await SaveAsync();
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Book> GetBookAsync(string name)
        {
            return await _context.Books.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
        
        public async Task<ICollection<Book>> GetBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }
        public async Task<bool> BookExists(int bookId)
        {
            return await _context.Books.AnyAsync(x => x.Id == bookId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return  saved > 0 ? true : false;
        }

        public async Task<bool> UpdateBook(int ownerId, int categoryId,Book book)
        {
            _context.Update(book);
            return await SaveAsync();
        }

        
    }
}
