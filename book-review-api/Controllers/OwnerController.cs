using AutoMapper;
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
