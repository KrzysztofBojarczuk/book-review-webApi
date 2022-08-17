using AutoMapper;
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
        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var getCat = await _categoryRepository.CategoryExists(categoryId);

            if (!getCat)
            {
                return NotFound("No Objects!!!!!!!!!!");
            }

            var catId = await _categoryRepository.GetCategory(categoryId);

            var category = _mapper.Map<CategoryDto>(catId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(category);
        }

        [HttpGet("book/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBookByCategoryId(int categoryId)
        {
            var bookId = await _categoryRepository.GetBookByCategory(categoryId);
            var books = _mapper.Map<IEnumerable<Book>>(bookId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(books);

        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreate)
        {
            if (categoryCreate == null)
            {
                return BadRequest(ModelState);
            }
            var category = await _categoryRepository.GetCategories();  //Błąd



            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            var getCat = await _categoryRepository.CreateCategory(categoryMap);
            if (!getCat)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");




        }


        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
                return BadRequest(ModelState);

            if (categoryId != updatedCategory.Id)
                return BadRequest(ModelState);
            var getCar = await _categoryRepository.CategoryExists(categoryId);
            if (!getCar)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = _mapper.Map<Category>(updatedCategory);
            var getcarMapped = await _categoryRepository.UpdateCategory(categoryMap);
            if (!getcarMapped)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
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
