# Book Library Management API - Copilot Instructions

This is a .NET Web API project for managing a book library system with full CRUD operations.

## Project Completed ✅

- [x] Verify that the copilot-instructions.md file in the .github directory is created.
- [x] Clarify Project Requirements - .NET Web API for book library management
- [x] Scaffold the Project - Created .NET Web API project structure
- [x] Customize the Project - Implemented controllers, DTOs, repositories, and models
- [x] Install Required Extensions - No additional extensions needed
- [x] Compile the Project - Project builds successfully
- [x] Create and Run Task - VS Code tasks configured for build and run
- [x] Launch the Project - Application running successfully on http://localhost:5000
- [x] Ensure Documentation is Complete - Comprehensive README.md created

## Project Overview

The Book Library Management API includes:
- RESTful API endpoints for book management
- Repository pattern for data access
- DTO pattern for API contracts
- Input validation and error handling
- Comprehensive documentation

## Key Components

- **Models**: `Book.cs` - Core entity
- **DTOs**: `BookDto.cs`, `CreateBookDto.cs`, `UpdateBookDto.cs`
- **Repository**: `IBookRepository.cs`, `BookRepository.cs`
- **Controller**: `BooksController.cs` - API endpoints
- **Configuration**: `Program.cs` - Dependency injection setup

## Available Commands

- `dotnet build BookLibraryApi.csproj` - Build the project
- `dotnet run --project BookLibraryApi.csproj` - Run the application
- `dotnet run --project BookLibraryApi.csproj --urls "http://localhost:5000"` - Run on specific port

The project is ready for development and testing!