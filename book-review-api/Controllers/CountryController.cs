using AutoMapper;
using book_review_api.Dto.CountryDtoMap;
using book_review_api.Interfaces;
using book_review_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace book_review_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public async Task<IActionResult> GetCountries()
        {
            var countiesGet = await _countryRepository.GetCountries();
            var countries = _mapper.Map<IEnumerable<CountryDto>>(countiesGet);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }
        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            var countries = await _countryRepository.CountryExists(countryId);

            if (!countries)
            {
                return NotFound("No Objects!!!!!!!!!!");
            }

            var countryToDelete = await _countryRepository.GetCountry(countryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getCountry = await _countryRepository.DeleteCountry(countryToDelete);
            if (!getCountry)
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }
            return NoContent();
        }

    }
}
