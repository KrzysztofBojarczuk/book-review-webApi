using AutoMapper;
using book_review_api.Dto;
using book_review_api.Dto.OwnerDto;
using book_review_api.Interfaces;
using book_review_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace book_review_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository,
            ICountryRepository countryRepository,
            IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public async Task<IActionResult> GetOwners()
        {
            var ownerGet = await _ownerRepository.GetOwnersAsync();
            var owners = _mapper.Map<IEnumerable<OwnerDto>>(ownerGet);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }



        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOwner(int ownerId)
        {
            var getOwner = await _ownerRepository.OwnerExistsAsync(ownerId);
            if (!getOwner)
                return NotFound();

            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("{ownerId}/book")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBookByOwner(int ownerId)
        {
            var getBook = await _ownerRepository.OwnerExistsAsync(ownerId);
            if (!getBook)
            {
                return NotFound();
            }
            var getOwner = await _ownerRepository.GetBookByOwnerAsync(ownerId); 
            var owner = _mapper.Map<IEnumerable<BookDto>>(getOwner);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }




        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);

            var owners = await _ownerRepository.GetOwnersAsync();
            var ownersTrim =  owners.Where(c => c.LastName.Trim().ToUpper() == ownerCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (owners != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = _mapper.Map<Owner>(ownerCreate);

            ownerMap.Country = await _countryRepository.GetCountry(countryId);

            var getOwnert = await _ownerRepository.CreateOwnerAsync(ownerMap);
            if (!getOwnert)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateOwner(int ownerId, [FromBody] OwnerDto updatedOwner)
        {
            if (updatedOwner == null)
                return BadRequest(ModelState);

            if (ownerId != updatedOwner.Id)
                return BadRequest(ModelState);


            var getOwner = await _ownerRepository.OwnerExistsAsync(ownerId);
            if (!getOwner)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var ownerMap = _mapper.Map<Owner>(updatedOwner);

            var getOwnerMapped = await _ownerRepository.UpdateOwnerAsync(ownerMap);
            if (!getOwnerMapped)
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteOwner(int ownerId)
        {
            var getOwner = await _ownerRepository.OwnerExistsAsync(ownerId);

            if (!getOwner)
            {
                return NotFound("No Objects!!!!!!!!!!");
            }

            var ownerToDelete = await _ownerRepository.GetOwner(ownerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getOwnerToDelete = await _ownerRepository.DeleteOwnerAsync(ownerToDelete);
            if (getOwnerToDelete == null)
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }

    }
}
