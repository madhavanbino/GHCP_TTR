using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Models;
using BookLibraryApi.DTOs;
using BookLibraryApi.Repositories;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// Added the comment.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            var bookDtos = books.Select(MapToDto);
            return Ok(bookDtos);
        }

        /// <summary>
        /// Get a book by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return Ok(MapToDto(book));
        }

        /// <summary>
        /// Get a book by ISBN
        /// </summary>
        [HttpGet("isbn/{isbn}")]
        public async Task<ActionResult<BookDto>> GetBookByISBN(string isbn)
        {
            var book = await _bookRepository.GetBookByISBNAsync(isbn);
            if (book == null)
            {
                return NotFound($"Book with ISBN {isbn} not found.");
            }

            return Ok(MapToDto(book));
        }

        /// <summary>
        /// Search books by title, author, or genre
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDto>>> SearchBooks([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var books = await _bookRepository.SearchBooksAsync(searchTerm);
            var bookDtos = books.Select(MapToDto);
            return Ok(bookDtos);
        }

        /// <summary>
        /// Search books by multiple search terms (title, author, genre, or description)
        /// </summary>
        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<BookDto>>> SearchBooksWithTerms([FromBody] string[] searchTerms)
        {
            if (searchTerms == null || searchTerms.Length == 0)
            {
                return BadRequest("Search terms array cannot be null or empty.");
            }

            var validTerms = searchTerms.Where(t => !string.IsNullOrWhiteSpace(t)).ToArray();
            if (validTerms.Length == 0)
            {
                return BadRequest("At least one valid search term is required.");
            }

            var books = await _bookRepository.SearchBooksAsync(searchTerms);
            var bookDtos = books.Select(MapToDto);
            return Ok(bookDtos);
        }

        /// <summary>
        /// Get books by genre
        /// </summary>
        [HttpGet("genre/{genre}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByGenre(string genre)
        {
            var books = await _bookRepository.GetBooksByGenreAsync(genre);
            var bookDtos = books.Select(MapToDto);
            return Ok(bookDtos);
        }

        /// <summary>
        /// Get available books
        /// </summary>
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAvailableBooks()
        {
            var books = await _bookRepository.GetAvailableBooksAsync();
            var bookDtos = books.Select(MapToDto);
            return Ok(bookDtos);
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto createBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if book with same ISBN already exists
            var existingBook = await _bookRepository.GetBookByISBNAsync(createBookDto.ISBN);
            if (existingBook != null)
            {
                return Conflict($"A book with ISBN {createBookDto.ISBN} already exists.");
            }

            var book = new Book
            {
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                ISBN = createBookDto.ISBN,
                PublishedDate = createBookDto.PublishedDate,
                Genre = createBookDto.Genre,
                Description = createBookDto.Description,
                TotalCopies = createBookDto.TotalCopies
            };

            var createdBook = await _bookRepository.CreateBookAsync(book);
            var bookDto = MapToDto(createdBook);

            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, bookDto);
        }

        /// <summary>
        /// Update a book
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateBook(int id, UpdateBookDto updateBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBook = await _bookRepository.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            // Check if updating ISBN conflicts with existing book
            if (!string.IsNullOrEmpty(updateBookDto.ISBN) && updateBookDto.ISBN != existingBook.ISBN)
            {
                var bookWithSameISBN = await _bookRepository.GetBookByISBNAsync(updateBookDto.ISBN);
                if (bookWithSameISBN != null)
                {
                    return Conflict($"A book with ISBN {updateBookDto.ISBN} already exists.");
                }
            }

            // Update only provided fields
            if (!string.IsNullOrEmpty(updateBookDto.Title))
                existingBook.Title = updateBookDto.Title;
            if (!string.IsNullOrEmpty(updateBookDto.Author))
                existingBook.Author = updateBookDto.Author;
            if (!string.IsNullOrEmpty(updateBookDto.ISBN))
                existingBook.ISBN = updateBookDto.ISBN;
            if (updateBookDto.PublishedDate.HasValue)
                existingBook.PublishedDate = updateBookDto.PublishedDate.Value;
            if (!string.IsNullOrEmpty(updateBookDto.Genre))
                existingBook.Genre = updateBookDto.Genre;
            if (!string.IsNullOrEmpty(updateBookDto.Description))
                existingBook.Description = updateBookDto.Description;
            if (updateBookDto.TotalCopies.HasValue)
                existingBook.TotalCopies = updateBookDto.TotalCopies.Value;
            if (updateBookDto.IsAvailable.HasValue)
                existingBook.IsAvailable = updateBookDto.IsAvailable.Value;

            var updatedBook = await _bookRepository.UpdateBookAsync(id, existingBook);
            return Ok(MapToDto(updatedBook!));
        }

        /// <summary>
        /// Delete a book
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var success = await _bookRepository.DeleteBookAsync(id);
            if (!success)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return NoContent();
        }

        private static BookDto MapToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublishedDate = book.PublishedDate,
                Genre = book.Genre,
                IsAvailable = book.IsAvailable,
                Description = book.Description,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies
            };
        }
    }
}