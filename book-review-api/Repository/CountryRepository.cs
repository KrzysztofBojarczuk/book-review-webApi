using AutoMapper;
using book_review_api.Data;
using book_review_api.Interfaces;
using book_review_api.Models;
using Microsoft.EntityFrameworkCore;

namespace book_review_api.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CountryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ICollection<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<bool> CountryExists(int id)
        {
            return await _context.Countries.AnyAsync(c => c.Id == id);
        }

        public Task<bool> CreateCountry(Country country)
        {
            throw new NotImplementedException();
        }

    

        public Task<Country> GetCountry(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Country> GetCountryByOwner(int ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Owner>> GetOwnersFromACountry(int countryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateCountry(Country country)
        {
            _context.Update(country);
            return await SaveAsync();
        }

        public async Task<bool> DeleteCountry(Country country)
        {
            _context.Remove(country);
            return await SaveAsync();
        }

    }
}
