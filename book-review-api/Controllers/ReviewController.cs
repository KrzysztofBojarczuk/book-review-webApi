﻿using AutoMapper;
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


    }
}
