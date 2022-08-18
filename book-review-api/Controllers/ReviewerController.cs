using AutoMapper;
using book_review_api.Dto.ReviewDto;
using book_review_api.Dto.Reviewer;
using book_review_api.Interfaces;
using book_review_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace book_review_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public async Task<IActionResult> GetReviewers()
        {
            var reviewersGet = await _reviewerRepository.GetReviewers();
            var reviewers = _mapper.Map<IEnumerable<ReviewerDto>>(reviewersGet);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBook(int reviewerId)
        {
            var reviwerAny = await _reviewerRepository.ReviewerExists(reviewerId);
            if (reviwerAny == null)
                return NotFound("Smutek");

            var revieverGet = await _reviewerRepository.GetReviewer(reviewerId);
            var reviewer = _mapper.Map<ReviewerDto>(revieverGet);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);
        }



        [HttpGet("{reviewerId}/reviews")]
        public async Task<IActionResult> GetReviewsByAReviewer(int reviewerId)
        {
            var getReviews = await _reviewerRepository.ReviewerExists(reviewerId);
            if (!getReviews)
                return NotFound();

            var reviewerMap = _reviewerRepository.GetReviewsByReviewer(reviewerId);
            var reviews = _mapper.Map<IEnumerable<ReviewDto>>(reviewerMap);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateReviewer([FromBody] ReviewerDto reviewerCreate)
        {
            if (reviewerCreate == null)
                return BadRequest(ModelState);


            var country = await _reviewerRepository.GetReviewers();
          
            var getCountry =  country.Where(c => c.LastName.Trim().ToUpper() == reviewerCreate.LastName.TrimEnd().ToUpper()).FirstOrDefault();
               

            if (getCountry != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

            var reviewerGet = await _reviewerRepository.CreateReviewer(reviewerMap);

            if (!reviewerGet)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateReviewer(int reviewerId, [FromBody] ReviewerDto updatedReviewer)
        {
            if (updatedReviewer == null)
                return BadRequest(ModelState);

            if (reviewerId != updatedReviewer.Id)
                return BadRequest(ModelState);

            var getReviever = await _reviewerRepository.ReviewerExists(reviewerId);
            if (!getReviever)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewerMap = _mapper.Map<Reviewer>(updatedReviewer);

            var getReview = await _reviewerRepository.UpdateReviewer(reviewerMap);

            if (!getReview)
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> DeleteReviewer(int reviewerId)
        {
            var getReviewer = await _reviewerRepository.ReviewerExists(reviewerId);

            if (!getReviewer)
            {
                return NotFound("No Objects!!!!!!!!!!");
            }
            var revieverToDelete = await _reviewerRepository.GetReviewer(reviewerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var reviewer = await _reviewerRepository.DeleteReviewer(revieverToDelete);
            if (!reviewer)
            {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            }
            return NoContent();

        }

    }
}
