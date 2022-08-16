﻿using AutoMapper;
using book_review_api.Dto.CategroyDroMap;
using book_review_api.Interfaces;
using book_review_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace book_review_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public async Task<IActionResult> GetCategories()
        {
            var catGet = await _categoryRepository.GetCategories();
            var categories = _mapper.Map<IEnumerable<CategoryDto>>(catGet);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var categoryGet = await _categoryRepository.CategoryExists(categoryId);
            if (!categoryGet)
            {
                return NotFound("No Objects!!!!!!!!!!");
            }

            var categoryToDelete = await _categoryRepository.GetCategory(categoryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cat = await _categoryRepository.DeleteCategory(categoryToDelete);

            if (!cat)
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }
            return NoContent();

        }
    }
}