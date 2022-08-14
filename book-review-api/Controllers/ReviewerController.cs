using AutoMapper;
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

    }
}
