using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Models;
using BookLibraryApi.Data;

namespace BookLibraryApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookLibraryContext _context;

        public BookRepository(BookLibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book?> GetBookByISBNAsync(string isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            book.AvailableCopies = book.TotalCopies;
            book.IsAvailable = book.AvailableCopies > 0;
            
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> UpdateBookAsync(int id, Book updatedBook)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
                return null;

            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.ISBN = updatedBook.ISBN;
            existingBook.PublishedDate = updatedBook.PublishedDate;
            existingBook.Genre = updatedBook.Genre;
            existingBook.Description = updatedBook.Description;
            existingBook.TotalCopies = updatedBook.TotalCopies;
            existingBook.AvailableCopies = updatedBook.AvailableCopies;
            existingBook.IsAvailable = existingBook.AvailableCopies > 0;

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();
            return existingBook;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(searchTerm) ||
                           b.Author.Contains(searchTerm) ||
                           b.Genre.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string[] searchTerms)
        {
            if (searchTerms == null || searchTerms.Length == 0)
            {
                return new List<Book>();
            }

            // Filter out null, empty, or whitespace-only terms
            var validTerms = searchTerms.Where(t => !string.IsNullOrWhiteSpace(t))
                                      .Select(t => t.Trim())
                                      .ToArray();

            if (validTerms.Length == 0)
            {
                return new List<Book>();
            }

            // Search for books that contain ANY of the search terms
            return await _context.Books
                .Where(b => validTerms.Any(term => 
                    b.Title.Contains(term) ||
                    b.Author.Contains(term) ||
                    b.Genre.Contains(term) ||
                    b.Description.Contains(term)))
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(string genre)
        {
            return await _context.Books
                .Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            return await _context.Books
                .Where(b => b.IsAvailable && b.AvailableCopies > 0)
                .ToListAsync();
        }
    }
}