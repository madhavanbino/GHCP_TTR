using Microsoft.AspNetCore.Mvc;
using Moq;
using BookLibraryApi.Controllers;
using BookLibraryApi.Models;
using BookLibraryApi.DTOs;
using BookLibraryApi.Repositories;

namespace BookLibraryApi.Tests.Controllers
{
    public class BooksControllerTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _controller = new BooksController(_mockRepository.Object);
        }

        [Fact]
        public async Task SearchBooksWithTerms_ValidTerms_ReturnsOkResult()
        {
            // Arrange
            var searchTerms = new[] { "gatsby", "orwell" };
            var expectedBooks = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    ISBN = "978-0-7432-7356-5",
                    PublishedDate = new DateTime(1925, 4, 10),
                    Genre = "Classic Literature",
                    Description = "A story of decadence and excess in 1920s America",
                    TotalCopies = 5,
                    AvailableCopies = 3,
                    IsAvailable = true
                },
                new Book
                {
                    Id = 2,
                    Title = "1984",
                    Author = "George Orwell",
                    ISBN = "978-0-452-28423-4",
                    PublishedDate = new DateTime(1949, 6, 8),
                    Genre = "Dystopian Fiction",
                    Description = "A dystopian social science fiction novel",
                    TotalCopies = 3,
                    AvailableCopies = 2,
                    IsAvailable = true
                }
            };

            _mockRepository.Setup(repo => repo.SearchBooksAsync(searchTerms))
                          .ReturnsAsync(expectedBooks);

            // Act
            var result = await _controller.SearchBooksWithTerms(searchTerms);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBooks = Assert.IsAssignableFrom<IEnumerable<BookDto>>(okResult.Value);
            Assert.Equal(2, returnedBooks.Count());
            
            var booksList = returnedBooks.ToList();
            Assert.Equal("The Great Gatsby", booksList[0].Title);
            Assert.Equal("1984", booksList[1].Title);
        }

        [Fact]
        public async Task SearchBooksWithTerms_NullTerms_ReturnsBadRequest()
        {
            // Arrange
            string[] searchTerms = null;

            // Act
            var result = await _controller.SearchBooksWithTerms(searchTerms);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Search terms array cannot be null or empty.", badRequestResult.Value);
        }

        [Fact]
        public async Task SearchBooksWithTerms_EmptyTerms_ReturnsBadRequest()
        {
            // Arrange
            var searchTerms = new string[0];

            // Act
            var result = await _controller.SearchBooksWithTerms(searchTerms);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Search terms array cannot be null or empty.", badRequestResult.Value);
        }

        [Fact]
        public async Task SearchBooksWithTerms_AllEmptyOrWhitespaceTerms_ReturnsBadRequest()
        {
            // Arrange
            var searchTerms = new[] { "", "   ", null };

            // Act
            var result = await _controller.SearchBooksWithTerms(searchTerms);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("At least one valid search term is required.", badRequestResult.Value);
        }

        [Fact]
        public async Task SearchBooksWithTerms_MixedValidAndInvalidTerms_ReturnsOkResult()
        {
            // Arrange
            var searchTerms = new[] { "gatsby", "", "   ", "orwell", null };
            var expectedBooks = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    ISBN = "978-0-7432-7356-5",
                    PublishedDate = new DateTime(1925, 4, 10),
                    Genre = "Classic Literature",
                    Description = "A story of decadence and excess",
                    TotalCopies = 5,
                    AvailableCopies = 3,
                    IsAvailable = true
                }
            };

            _mockRepository.Setup(repo => repo.SearchBooksAsync(searchTerms))
                          .ReturnsAsync(expectedBooks);

            // Act
            var result = await _controller.SearchBooksWithTerms(searchTerms);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBooks = Assert.IsAssignableFrom<IEnumerable<BookDto>>(okResult.Value);
            Assert.Single(returnedBooks);
        }
    }
}