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

    }
}
