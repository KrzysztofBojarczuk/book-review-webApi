using book_review_api.Models;

namespace book_review_api.Interfaces
{
    public interface IBookRepository
    {
        Task<ICollection<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(int id);
        Task<Book> GetBookAsync(string name);
        Task<bool> BookExists(int bookId);
        Task<bool> CreateBookAsync( Book book);
        Task<bool> UpdateBook(int ownerId, int categoryId, Book book);
        Task<bool> DeleteBookAsync(Book book);
       Task<bool> SaveAsync();
    }
}
