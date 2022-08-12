using AutoMapper;
using book_review_api.Dto;
using book_review_api.Interfaces;
using book_review_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace book_review_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetBooksAsync();
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(booksDto);
        }
        [HttpGet("{bookId}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBook(int bookId)
        {
            var idbook = await _bookRepository.BookExists(bookId);
            if (!idbook)
                return NotFound("Smutek");

            var book = await _bookRepository.GetBookAsync(bookId);

            //if (book == null)
            //    return NotFound();

            var bookMap = _mapper.Map<BookDto>(book);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookMap);


        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> CreateBook([FromBody] BookCreateDto bookCreate)
        {
            if (bookCreate == null)
                return BadRequest(ModelState);

            var bookMap = _mapper.Map<Book>(bookCreate);
            await _bookRepository.CreateBookAsync(bookMap);


            return Ok("Successfully created");
        }
    }
}
