﻿using book_review_api.Models;

namespace book_review_api.Interfaces
{
    public interface ICountryRepository
    {
        Task<ICollection<Country>> GetCountries();
        Task<Country> GetCountry(int id);
        Task<Country> GetCountryByOwner(int ownerId);
        Task<ICollection<Owner>> GetOwnersFromACountry(int countryId);
        Task<bool> CountryExists(int id);
        Task<bool> CreateCountry(Country country);
        Task<bool> UpdateCountry(Country country);
        Task<bool> DeleteCountry(Country country);
        Task<bool> SaveAsync();
    }
}
