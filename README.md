# Book Library Management API

A simple and clean .NET Web API for managing a book library system. This RESTful API provides endpoints for managing books with full CRUD operations, search functionality, and availability tracking.

## 📋 Table of Contents

- [Description](#description)
- [Project Structure](#project-structure)
- [Patterns and Practices](#patterns-and-practices)
- [Classes and Components](#classes-and-components)
- [API Endpoints](#api-endpoints)
- [Getting Started](#getting-started)
- [Build and Run](#build-and-run)
- [Testing](#testing)
- [Publishing](#publishing)
- [Sample Requests](#sample-requests)

## 📖 Description

The Book Library Management API is a .NET Web API application built to manage a collection of books in a library system. It provides functionality to:

- Manage book inventory (Create, Read, Update, Delete)
- Search books by title, author, or genre
- Track book availability and copies
- Filter books by genre or availability status
- Validate data input with comprehensive error handling

## 🏗️ Project Structure

```
BookLibraryApi/
├── .github/
│   └── copilot-instructions.md    # Copilot configuration
├── Controllers/
│   └── BooksController.cs         # API endpoints controller
├── DTOs/
│   ├── BookDto.cs                 # Data transfer object for book responses
│   ├── CreateBookDto.cs           # DTO for creating new books
│   └── UpdateBookDto.cs           # DTO for updating existing books
├── Models/
│   └── Book.cs                    # Core book entity model
├── Repositories/
│   ├── IBookRepository.cs         # Repository interface
│   └── BookRepository.cs          # In-memory repository implementation
├── Properties/
│   └── launchSettings.json        # Application launch configuration
├── appsettings.json               # Application configuration
├── appsettings.Development.json   # Development environment settings
├── Program.cs                     # Application entry point and configuration
├── BookLibraryApi.csproj          # Project file
├── BookLibraryApi.http            # HTTP test requests
└── README.md                      # Project documentation
```

## 🎯 Patterns and Practices

### Design Patterns Used

1. **Repository Pattern**: Abstracts data access logic and provides a centralized point for data operations
2. **Dependency Injection**: Used for loose coupling and better testability
3. **DTO Pattern**: Separates internal models from API contracts
4. **RESTful API Design**: Follows REST conventions for HTTP methods and status codes

### Best Practices Implemented

- **Separation of Concerns**: Clear separation between controllers, business logic, and data access
- **Input Validation**: Data annotations for request validation
- **Error Handling**: Proper HTTP status codes and error messages
- **Async/Await**: Asynchronous programming for better performance
- **Clean Code**: Meaningful names, single responsibility principle
- **API Documentation**: Comprehensive XML documentation comments

## 🔧 Classes and Components

### Models

#### `Book.cs`
Core entity representing a book in the library system.

**Properties:**
- `Id` (int): Unique identifier
- `Title` (string): Book title
- `Author` (string): Book author
- `ISBN` (string): International Standard Book Number
- `PublishedDate` (DateTime): Publication date
- `Genre` (string): Book genre/category
- `IsAvailable` (bool): Availability status
- `Description` (string): Book description
- `TotalCopies` (int): Total number of copies
- `AvailableCopies` (int): Number of available copies

### DTOs (Data Transfer Objects)

#### `BookDto.cs`
Response DTO that represents book data sent to clients.

#### `CreateBookDto.cs`
Request DTO for creating new books with validation attributes:
- Required fields: Title, Author, ISBN, PublishedDate
- String length validations
- Range validation for TotalCopies

#### `UpdateBookDto.cs`
Request DTO for updating existing books with optional properties and validation.

### Repository Layer

#### `IBookRepository.cs`
Interface defining the contract for book data operations:
- `GetAllBooksAsync()`: Retrieve all books
- `GetBookByIdAsync(int id)`: Get book by ID
- `GetBookByISBNAsync(string isbn)`: Get book by ISBN
- `CreateBookAsync(Book book)`: Create new book
- `UpdateBookAsync(int id, Book book)`: Update existing book
- `DeleteBookAsync(int id)`: Delete book
- `SearchBooksAsync(string searchTerm)`: Search books
- `GetBooksByGenreAsync(string genre)`: Filter by genre
- `GetAvailableBooksAsync()`: Get available books

#### `BookRepository.cs`
In-memory implementation of the repository with sample data and full CRUD operations.

### Controllers

#### `BooksController.cs`
RESTful API controller that handles HTTP requests and responses:
- Dependency injection of `IBookRepository`
- Full CRUD operations with proper HTTP status codes
- Input validation and error handling
- DTO mapping between models and responses

## 🚀 API Endpoints

### Base URL: `http://localhost:5000/api/books`

| Method | Endpoint | Description | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| GET | `/api/books` | Get all books | None | Array of BookDto |
| GET | `/api/books/{id}` | Get book by ID | None | BookDto |
| GET | `/api/books/isbn/{isbn}` | Get book by ISBN | None | BookDto |
| GET | `/api/books/search?searchTerm={term}` | Search books | None | Array of BookDto |
| GET | `/api/books/genre/{genre}` | Get books by genre | None | Array of BookDto |
| GET | `/api/books/available` | Get available books | None | Array of BookDto |
| POST | `/api/books` | Create new book | CreateBookDto | BookDto |
| PUT | `/api/books/{id}` | Update book | UpdateBookDto | BookDto |
| DELETE | `/api/books/{id}` | Delete book | None | 204 No Content |

### HTTP Status Codes

- `200 OK`: Successful GET/PUT requests
- `201 Created`: Successful POST requests
- `204 No Content`: Successful DELETE requests
- `400 Bad Request`: Invalid input data
- `404 Not Found`: Resource not found
- `409 Conflict`: Duplicate ISBN

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio Code](https://code.visualstudio.com/) (recommended)
- [C# Dev Kit Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) (recommended)

### Installation

1. **Clone or download the project**
2. **Navigate to the project directory**
   ```bash
   cd "c:\Bino\Technology Services\GHCP\.Net DEMO"
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore BookLibraryApi.csproj
   ```

## 🔨 Build and Run

### Build the Project

```bash
# Build the project
dotnet build BookLibraryApi.csproj

# Build in Release mode
dotnet build BookLibraryApi.csproj --configuration Release
```

### Run the Application

```bash
# Run in Development mode (default port)
dotnet run --project BookLibraryApi.csproj

# Run on specific port
dotnet run --project BookLibraryApi.csproj --urls "http://localhost:5000"

# Run in Production mode
dotnet run --project BookLibraryApi.csproj --environment Production
```

### Using VS Code Tasks

The project includes pre-configured VS Code tasks:

- **Build Task**: `Ctrl+Shift+P` → "Tasks: Run Task" → "build"
- **Run Task**: `Ctrl+Shift+P` → "Tasks: Run Task" → "run"

### Verify the Application

Once running, you can verify the API is working:

1. **Check the application is running**: Navigate to `http://localhost:5000/api/books`
2. **View OpenAPI documentation**: Navigate to `http://localhost:5000/openapi/v1.json`

## 🧪 Testing

### Manual Testing

Use the included `BookLibraryApi.http` file with VS Code REST Client extension or tools like:
- **Postman**
- **curl**
- **HTTPie**
- **VS Code REST Client**

### Sample Test Commands

```bash
# Test GET all books
curl -X GET "http://localhost:5000/api/books"

# Test GET book by ID
curl -X GET "http://localhost:5000/api/books/1"

# Test search functionality
curl -X GET "http://localhost:5000/api/books/search?searchTerm=gatsby"

# Test POST new book
curl -X POST "http://localhost:5000/api/books" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "New Book",
    "author": "Test Author",
    "isbn": "9780123456789",
    "publishedDate": "2024-01-01",
    "genre": "Fiction",
    "description": "A test book",
    "totalCopies": 5
  }'
```

### Unit Testing (Future Enhancement)

To add unit tests, create a test project:

```bash
# Create test project
dotnet new xunit -n BookLibraryApi.Tests

# Add reference to main project
dotnet add BookLibraryApi.Tests/BookLibraryApi.Tests.csproj reference BookLibraryApi.csproj

# Run tests
dotnet test
```

## 📦 Publishing

### Publish for Production

```bash
# Publish for current runtime
dotnet publish BookLibraryApi.csproj --configuration Release --output ./publish

# Publish for specific runtime (Windows x64)
dotnet publish BookLibraryApi.csproj --configuration Release --runtime win-x64 --output ./publish

# Publish for Linux x64
dotnet publish BookLibraryApi.csproj --configuration Release --runtime linux-x64 --output ./publish

# Self-contained deployment
dotnet publish BookLibraryApi.csproj --configuration Release --runtime win-x64 --self-contained true --output ./publish
```

### Docker Deployment (Optional)

Create a `Dockerfile`:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY BookLibraryApi.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookLibraryApi.dll"]
```

Build and run Docker container:

```bash
# Build Docker image
docker build -t book-library-api .

# Run Docker container
docker run -p 5000:80 book-library-api
```

## 📝 Sample Requests

### Get All Books

```http
GET http://localhost:5000/api/books
Content-Type: application/json
```

### Create a New Book

```http
POST http://localhost:5000/api/books
Content-Type: application/json

{
  "title": "The Catcher in the Rye",
  "author": "J.D. Salinger",
  "isbn": "9780316769174",
  "publishedDate": "1951-07-16",
  "genre": "Classic Literature",
  "description": "A controversial novel about teenage rebellion",
  "totalCopies": 3
}
```

### Update a Book

```http
PUT http://localhost:5000/api/books/1
Content-Type: application/json

{
  "title": "The Great Gatsby - Updated Edition",
  "description": "An updated description of this classic American novel"
}
```

### Search Books

```http
GET http://localhost:5000/api/books/search?searchTerm=gatsby
Content-Type: application/json
```

### Delete a Book

```http
DELETE http://localhost:5000/api/books/1
Content-Type: application/json
```

## 🛠️ Development Notes

### Future Enhancements

1. **Database Integration**: Replace in-memory repository with Entity Framework Core
2. **Authentication**: Add JWT authentication and authorization
3. **Logging**: Implement structured logging with Serilog
4. **Caching**: Add Redis caching for improved performance
5. **Validation**: Enhance validation with FluentValidation
6. **API Versioning**: Implement API versioning strategy
7. **Rate Limiting**: Add rate limiting for API protection
8. **Health Checks**: Implement health check endpoints
9. **Swagger/OpenAPI**: Add comprehensive API documentation
10. **Unit Tests**: Add comprehensive test coverage

### Configuration

The application uses standard .NET configuration:
- `appsettings.json`: Base configuration
- `appsettings.Development.json`: Development-specific settings
- Environment variables can override any configuration

### Error Handling

The API returns appropriate HTTP status codes and error messages:
- Validation errors return detailed model state information
- Not found errors return descriptive messages
- Conflict errors indicate duplicate resources

---

## 📄 License

This project is for educational and demonstration purposes.

## 🤝 Contributing

This is a sample project for demonstration purposes. For production use, consider implementing the suggested enhancements above.

---

**Happy Coding! 🚀**