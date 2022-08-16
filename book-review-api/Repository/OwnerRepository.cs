using book_review_api.Data;
using book_review_api.Interfaces;
using book_review_api.Models;
using Microsoft.EntityFrameworkCore;

namespace book_review_api.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<ICollection<Owner>> GetOwnersAsync()
        {
            return await _context.Owners.ToListAsync();
        }
        public async Task<Owner> GetOwner(int ownerId)
        {
            return await _context.Owners.Where(o => o.Id == ownerId).FirstOrDefaultAsync();
        }
        public async Task<ICollection<Book>> GetBookByOwnerAsync(int ownerId)
        {
            return await _context.BookOwners.Where(p => p.Owner.Id == ownerId).Select(o => o.Book).ToListAsync();
        }

        public async Task<bool> CreateOwnerAsync(Owner owner)
        {
            _context.Add(owner);
            return await SaveAsync();

        }
        public async Task<ICollection<Owner>> GetOwnerOfABook(int bookId)
        {
            return await _context.BookOwners.Where(p => p.Book.Id == bookId).Select(o => o.Owner).ToListAsync();
        }

        public async Task<bool> OwnerExistsAsync(int ownerId)
        {
            return await _context.Owners.AnyAsync(x => x.Id == ownerId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateOwnerAsync(Owner owner)
        {
             _context.Update(owner);
            return await SaveAsync();
        }

        public async Task<bool> DeleteOwnerAsync(Owner owner)
        {
            _context.Remove(owner);
            return await SaveAsync();
        }

    }
}
