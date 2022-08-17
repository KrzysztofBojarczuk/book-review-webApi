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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IBookRepository _bookRepository;

        public ReviewController(IReviewRepository reviewRepository,
            IMapper mapper,
            IBookRepository bookRepository,
            IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _reviewerRepository = reviewerRepository;
            _bookRepository = bookRepository;
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

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBook(int reviewId)
        {
            var reviewAny = await _reviewRepository.ReviewExists(reviewId);
            if (reviewAny == null)
                return NotFound("Smutek");

            var reviewGet = await _reviewRepository.GetReview(reviewId);
            var review = _mapper.Map<ReviewDto>(reviewGet);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }
        [HttpGet("book/{bookId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewsForABook(int bookId)
        {
            var reviewsGet = await _reviewRepository.GetReviewsOfABook(bookId);
            var reviews = _mapper.Map<IEnumerable<ReviewDto>>(reviewsGet);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateReview([FromQuery] int reviewerId, [FromQuery] int bookId, [FromBody] ReviewCreateDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            var reviews = _reviewRepository.GetReviews();

                //.Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
                //.FirstOrDefault();

            if (reviews != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.Book = await _bookRepository.GetBookAsync(bookId);
            reviewMap.Reviewer = await _reviewerRepository.GetReviewer(reviewerId);

            var getRevi = await _reviewRepository.CreateReview(reviewMap);
            if (!getRevi)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null)
                return BadRequest(ModelState);

            if (reviewId != updatedReview.Id)
                return BadRequest(ModelState);

            var getReview = await _reviewRepository.ReviewExists(reviewId);
            if (!getReview)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(updatedReview);

            var getReviewMapped = await _reviewRepository.UpdateReview(reviewMap);
            if (!getReviewMapped)
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var getReview = await _reviewRepository.ReviewExists(reviewId);

            if (!getReview)
            {
                return NotFound("No Objects!!!!!!!!!!");
            }

            var reviewToDelete = await _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getReviewToDelete = await _reviewRepository.DeleteReview(reviewToDelete);

            if (!getReviewToDelete)
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }
            return NoContent();
            
        }

    }
}
