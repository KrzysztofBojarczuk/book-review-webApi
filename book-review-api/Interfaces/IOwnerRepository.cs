using book_review_api.Models;

namespace book_review_api.Interfaces
{
    public interface IOwnerRepository
    {

        Task<ICollection<Owner>> GetOwnersAsync();
        Task<Owner> GetOwner(int ownerId);
        Task<ICollection<Owner>> GetOwnerOfABook(int bookId);
        Task<ICollection<Book>> GetBookByOwnerAsync(int ownerId);
        Task<bool> OwnerExistsAsync(int ownerId);
        Task<bool> CreateOwnerAsync(Owner owner);
        Task<bool> UpdateOwnerAsync(Owner owner);
        Task<bool> DeleteOwnerAsync(Owner owner);
        Task<bool> SaveAsync();
    }
}
