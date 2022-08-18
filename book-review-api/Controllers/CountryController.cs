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


        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCountry(int countryId)
        {

            var country = await _countryRepository.GetCountry(countryId);
            if (country == null)
                return NotFound();

            var countryMap = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countryMap);
        }

        [HttpGet("/owners/{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Country))]
        public async Task<IActionResult> GetCountryOfAnOwner(int ownerId)
        {
            var getCountry = await _countryRepository.GetCountryByOwner(ownerId);
            var country = _mapper.Map<CountryDto>(getCountry);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
                return BadRequest(ModelState);

            var country = await _countryRepository.GetCountries();

            var getContryTrim = country.Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Country>(countryCreate);

            var countryMapGet = await _countryRepository.CreateCountry(countryMap);
            if (!countryMapGet)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCategory(int countryId, [FromBody] CountryDto updatedCountry)
        {
            if (updatedCountry == null)
                return BadRequest(ModelState);

            if (countryId != updatedCountry.Id)
                return BadRequest(ModelState);

            var getcoutnryExist = await _countryRepository.CountryExists(countryId);
            if (!getcoutnryExist)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var countryMap = _mapper.Map<Country>(updatedCountry);

            var getCountry = await _countryRepository.UpdateCountry(countryMap);
            if (!getCountry)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
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
