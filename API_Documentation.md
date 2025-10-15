# Book Library API Documentation

A comprehensive RESTful API for managing a book library system with full CRUD operations, search functionality, and availability tracking.

## Base Information

- **Base URL**: `http://localhost:5000`
- **API Version**: v1
- **Content Type**: `application/json`
- **Authentication**: None (for demo purposes)

## Data Models

### BookDto (Response Model)

```json
{
  "id": 1,
  "title": "string",
  "author": "string",
  "isbn": "string",
  "publishedDate": "2024-01-01T00:00:00Z",
  "genre": "string",
  "isAvailable": true,
  "description": "string",
  "totalCopies": 5,
  "availableCopies": 3
}
```

### CreateBookDto (Request Model)

```json
{
  "title": "string (required, max 200 chars)",
  "author": "string (required, max 100 chars)",
  "isbn": "string (required, max 13 chars)",
  "publishedDate": "2024-01-01T00:00:00Z (required)",
  "genre": "string (optional, max 50 chars)",
  "description": "string (optional, max 1000 chars)",
  "totalCopies": 1 (optional, min 1, default 1)
}
```

### UpdateBookDto (Request Model)

```json
{
  "title": "string (optional, max 200 chars)",
  "author": "string (optional, max 100 chars)",
  "isbn": "string (optional, max 13 chars)",
  "publishedDate": "2024-01-01T00:00:00Z (optional)",
  "genre": "string (optional, max 50 chars)",
  "description": "string (optional, max 1000 chars)",
  "totalCopies": 1 (optional, min 1)",
  "isAvailable": true (optional)
}
```

## API Endpoints

### 1. Get All Books

Retrieves all books in the library.

**Endpoint**: `GET /api/books`

**Response**: 
- **200 OK**: Array of BookDto objects
- **500 Internal Server Error**: Server error

#### cURL Example
```bash
curl -X GET "http://localhost:5000/api/books" \
  -H "accept: application/json"
```

#### JavaScript (Axios) Example
```javascript
import axios from 'axios';

const getAllBooks = async () => {
  try {
    const response = await axios.get('http://localhost:5000/api/books');
    console.log('Books:', response.data);
    return response.data;
  } catch (error) {
    console.error('Error fetching books:', error.response?.data || error.message);
    throw error;
  }
};

// Usage
getAllBooks();
```

#### Sample Response
```json
[
  {
    "id": 1,
    "title": "The Great Gatsby",
    "author": "F. Scott Fitzgerald",
    "isbn": "9780743273565",
    "publishedDate": "1925-04-10T00:00:00",
    "genre": "Classic Literature",
    "isAvailable": true,
    "description": "A classic American novel set in the Jazz Age",
    "totalCopies": 5,
    "availableCopies": 3
  },
  {
    "id": 2,
    "title": "To Kill a Mockingbird",
    "author": "Harper Lee",
    "isbn": "9780061120084",
    "publishedDate": "1960-07-11T00:00:00",
    "genre": "Classic Literature",
    "isAvailable": true,
    "description": "A gripping tale of racial injustice and childhood innocence",
    "totalCopies": 3,
    "availableCopies": 2
  }
]
```

---

### 2. Get Book by ID

Retrieves a specific book by its ID.

**Endpoint**: `GET /api/books/{id}`

**Parameters**:
- `id` (path, required): Integer - The book ID

**Response**: 
- **200 OK**: BookDto object
- **404 Not Found**: Book not found
- **400 Bad Request**: Invalid ID format

#### cURL Example
```bash
curl -X GET "http://localhost:5000/api/books/1" \
  -H "accept: application/json"
```

#### JavaScript (Axios) Example
```javascript
const getBookById = async (bookId) => {
  try {
    const response = await axios.get(`http://localhost:5000/api/books/${bookId}`);
    console.log('Book:', response.data);
    return response.data;
  } catch (error) {
    if (error.response?.status === 404) {
      console.error('Book not found');
    } else {
      console.error('Error fetching book:', error.response?.data || error.message);
    }
    throw error;
  }
};

// Usage
getBookById(1);
```

#### Sample Response
```json
{
  "id": 1,
  "title": "The Great Gatsby",
  "author": "F. Scott Fitzgerald",
  "isbn": "9780743273565",
  "publishedDate": "1925-04-10T00:00:00",
  "genre": "Classic Literature",
  "isAvailable": true,
  "description": "A classic American novel set in the Jazz Age",
  "totalCopies": 5,
  "availableCopies": 3
}
```

---

### 3. Get Book by ISBN

Retrieves a specific book by its ISBN.

**Endpoint**: `GET /api/books/isbn/{isbn}`

**Parameters**:
- `isbn` (path, required): String - The book ISBN

**Response**: 
- **200 OK**: BookDto object
- **404 Not Found**: Book not found

#### cURL Example
```bash
curl -X GET "http://localhost:5000/api/books/isbn/9780743273565" \
  -H "accept: application/json"
```

#### JavaScript (Axios) Example
```javascript
const getBookByISBN = async (isbn) => {
  try {
    const response = await axios.get(`http://localhost:5000/api/books/isbn/${isbn}`);
    console.log('Book:', response.data);
    return response.data;
  } catch (error) {
    if (error.response?.status === 404) {
      console.error('Book not found');
    } else {
      console.error('Error fetching book:', error.response?.data || error.message);
    }
    throw error;
  }
};

// Usage
getBookByISBN('9780743273565');
```

---

### 4. Search Books

Search for books by title, author, or genre.

**Endpoint**: `GET /api/books/search?searchTerm={term}`

**Parameters**:
- `searchTerm` (query, required): String - Search term to look for in title, author, or genre

**Response**: 
- **200 OK**: Array of BookDto objects
- **400 Bad Request**: Empty search term

#### cURL Example
```bash
curl -X GET "http://localhost:5000/api/books/search?searchTerm=gatsby" \
  -H "accept: application/json"
```

#### JavaScript (Axios) Example
```javascript
const searchBooks = async (searchTerm) => {
  try {
    const response = await axios.get('http://localhost:5000/api/books/search', {
      params: { searchTerm }
    });
    console.log('Search results:', response.data);
    return response.data;
  } catch (error) {
    if (error.response?.status === 400) {
      console.error('Search term cannot be empty');
    } else {
      console.error('Error searching books:', error.response?.data || error.message);
    }
    throw error;
  }
};

// Usage
searchBooks('gatsby');
searchBooks('Harper Lee');
searchBooks('Classic Literature');
```

---

### 5. Search Books with Multiple Terms

Search for books using multiple search terms. Returns books that match ANY of the provided search terms in title, author, genre, or description.

**Endpoint**: `POST /api/books/search`

**Request Body**: Array of strings (search terms)

```json
[
  "string1",
  "string2",
  "string3"
]
```

**Response**: 
- **200 OK**: Array of BookDto objects
- **400 Bad Request**: Empty or invalid search terms array

#### cURL Example
```bash
curl -X POST "http://localhost:5000/api/books/search" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d '["gatsby", "1984", "fantasy"]'
```

#### JavaScript (Axios) Example
```javascript
const searchBooksWithTerms = async (searchTerms) => {
  try {
    const response = await axios.post('http://localhost:5000/api/books/search', searchTerms);
    console.log('Search results:', response.data);
    return response.data;
  } catch (error) {
    if (error.response?.status === 400) {
      console.error('Search terms array cannot be empty or invalid');
    } else {
      console.error('Error searching books:', error.response?.data || error.message);
    }
    throw error;
  }
};

// Usage
searchBooksWithTerms(['gatsby', 'orwell', 'fiction']);
searchBooksWithTerms(['Harper Lee', 'Classic Literature']);
searchBooksWithTerms(['science', 'technology', 'programming']);
```

#### Sample Request
```json
["gatsby", "orwell", "fantasy"]
```

#### Sample Response
```json
[
  {
    "id": 1,
    "title": "The Great Gatsby",
    "author": "F. Scott Fitzgerald",
    "isbn": "978-0-7432-7356-5",
    "publishedDate": "1925-04-10T00:00:00Z",
    "genre": "Classic Literature",
    "isAvailable": true,
    "description": "A story of decadence and excess in 1920s America",
    "totalCopies": 5,
    "availableCopies": 3
  },
  {
    "id": 2,
    "title": "1984",
    "author": "George Orwell",
    "isbn": "978-0-452-28423-4",
    "publishedDate": "1949-06-08T00:00:00Z",
    "genre": "Dystopian Fiction",
    "isAvailable": true,
    "description": "A dystopian social science fiction novel",
    "totalCopies": 3,
    "availableCopies": 2
  }
]
```

---

### 6. Get Books by Genre

Retrieves all books of a specific genre.

**Endpoint**: `GET /api/books/genre/{genre}`

**Parameters**:
- `genre` (path, required): String - The genre to filter by

**Response**: 
- **200 OK**: Array of BookDto objects

#### cURL Example
```bash
curl -X GET "http://localhost:5000/api/books/genre/Classic%20Literature" \
  -H "accept: application/json"
```

#### JavaScript (Axios) Example
```javascript
const getBooksByGenre = async (genre) => {
  try {
    const response = await axios.get(`http://localhost:5000/api/books/genre/${encodeURIComponent(genre)}`);
    console.log(`Books in ${genre}:`, response.data);
    return response.data;
  } catch (error) {
    console.error('Error fetching books by genre:', error.response?.data || error.message);
    throw error;
  }
};

// Usage
getBooksByGenre('Classic Literature');
getBooksByGenre('Science Fiction');
```

---

### 7. Get Available Books

Retrieves all books that are currently available for borrowing.

**Endpoint**: `GET /api/books/available`

**Response**: 
- **200 OK**: Array of BookDto objects

#### cURL Example
```bash
curl -X GET "http://localhost:5000/api/books/available" \
  -H "accept: application/json"
```

#### JavaScript (Axios) Example
```javascript
const getAvailableBooks = async () => {
  try {
    const response = await axios.get('http://localhost:5000/api/books/available');
    console.log('Available books:', response.data);
    return response.data;
  } catch (error) {
    console.error('Error fetching available books:', error.response?.data || error.message);
    throw error;
  }
};

// Usage
getAvailableBooks();
```

---

### 8. Create New Book

Creates a new book in the library.

**Endpoint**: `POST /api/books`

**Request Body**: CreateBookDto object

**Response**: 
- **201 Created**: BookDto object with Location header
- **400 Bad Request**: Invalid request data or validation errors
- **409 Conflict**: Book with same ISBN already exists

#### cURL Example
```bash
curl -X POST "http://localhost:5000/api/books" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "The Catcher in the Rye",
    "author": "J.D. Salinger",
    "isbn": "9780316769174",
    "publishedDate": "1951-07-16T00:00:00Z",
    "genre": "Classic Literature",
    "description": "A controversial novel about teenage rebellion",
    "totalCopies": 3
  }'
```

#### JavaScript (Axios) Example
```javascript
const createBook = async (bookData) => {
  try {
    const response = await axios.post('http://localhost:5000/api/books', bookData, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    console.log('Book created:', response.data);
    return response.data;
  } catch (error) {
    if (error.response?.status === 400) {
      console.error('Validation errors:', error.response.data);
    } else if (error.response?.status === 409) {
      console.error('Book with this ISBN already exists');
    } else {
      console.error('Error creating book:', error.response?.data || error.message);
    }
    throw error;
  }
};

// Usage
const newBook = {
  title: "The Catcher in the Rye",
  author: "J.D. Salinger",
  isbn: "9780316769174",
  publishedDate: "1951-07-16T00:00:00Z",
  genre: "Classic Literature",
  description: "A controversial novel about teenage rebellion",
  totalCopies: 3
};

createBook(newBook);
```

#### Sample Request Body
```json
{
  "title": "The Catcher in the Rye",
  "author": "J.D. Salinger",
  "isbn": "9780316769174",
  "publishedDate": "1951-07-16T00:00:00Z",
  "genre": "Classic Literature",
  "description": "A controversial novel about teenage rebellion",
  "totalCopies": 3
}
```

#### Sample Response
```json
{
  "id": 4,
  "title": "The Catcher in the Rye",
  "author": "J.D. Salinger",
  "isbn": "9780316769174",
  "publishedDate": "1951-07-16T00:00:00",
  "genre": "Classic Literature",
  "isAvailable": true,
  "description": "A controversial novel about teenage rebellion",
  "totalCopies": 3,
  "availableCopies": 3
}
```

---

### 9. Update Book

Updates an existing book (partial update supported).

**Endpoint**: `PUT /api/books/{id}`

**Parameters**:
- `id` (path, required): Integer - The book ID

**Request Body**: UpdateBookDto object

**Response**: 
- **200 OK**: Updated BookDto object
- **400 Bad Request**: Invalid request data or validation errors
- **404 Not Found**: Book not found
- **409 Conflict**: ISBN conflicts with existing book

#### cURL Example
```bash
curl -X PUT "http://localhost:5000/api/books/1" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "The Great Gatsby - Updated Edition",
    "description": "An updated description of this classic American novel",
    "totalCopies": 6
  }'
```

#### JavaScript (Axios) Example
```javascript
const updateBook = async (bookId, updateData) => {
  try {
    const response = await axios.put(`http://localhost:5000/api/books/${bookId}`, updateData, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    console.log('Book updated:', response.data);
    return response.data;
  } catch (error) {
    if (error.response?.status === 400) {
      console.error('Validation errors:', error.response.data);
    } else if (error.response?.status === 404) {
      console.error('Book not found');
    } else if (error.response?.status === 409) {
      console.error('ISBN conflicts with existing book');
    } else {
      console.error('Error updating book:', error.response?.data || error.message);
    }
    throw error;
  }
};

// Usage - Partial update example
const updateData = {
  title: "The Great Gatsby - Updated Edition",
  description: "An updated description of this classic American novel",
  totalCopies: 6
};

updateBook(1, updateData);
```

#### Sample Request Body (Partial Update)
```json
{
  "title": "The Great Gatsby - Updated Edition",
  "description": "An updated description of this classic American novel",
  "totalCopies": 6
}
```

---

### 10. Delete Book

Deletes a book from the library.

**Endpoint**: `DELETE /api/books/{id}`

**Parameters**:
- `id` (path, required): Integer - The book ID

**Response**: 
- **204 No Content**: Book successfully deleted
- **404 Not Found**: Book not found

#### cURL Example
```bash
curl -X DELETE "http://localhost:5000/api/books/1" \
  -H "accept: application/json"
```

#### JavaScript (Axios) Example
```javascript
const deleteBook = async (bookId) => {
  try {
    const response = await axios.delete(`http://localhost:5000/api/books/${bookId}`);
    console.log('Book deleted successfully');
    return true;
  } catch (error) {
    if (error.response?.status === 404) {
      console.error('Book not found');
    } else {
      console.error('Error deleting book:', error.response?.data || error.message);
    }
    throw error;
  }
};

// Usage
deleteBook(1);
```

---

## Error Responses

### Validation Error (400 Bad Request)
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "00-...",
  "errors": {
    "Title": ["The Title field is required."],
    "ISBN": ["The field ISBN must be a string with a maximum length of 13."]
  }
}
```

### Not Found Error (404)
```json
{
  "error": "Book with ID 999 not found."
}
```

### Conflict Error (409)
```json
{
  "error": "A book with ISBN 9780743273565 already exists."
}
```

---

## Complete JavaScript Example Class

Here's a complete JavaScript class that demonstrates all API operations:

```javascript
import axios from 'axios';

class BookLibraryAPI {
  constructor(baseURL = 'http://localhost:5000') {
    this.baseURL = baseURL;
    this.api = axios.create({
      baseURL: this.baseURL,
      headers: {
        'Content-Type': 'application/json'
      }
    });
  }

  // Get all books
  async getAllBooks() {
    try {
      const response = await this.api.get('/api/books');
      return response.data;
    } catch (error) {
      this.handleError('Error fetching all books', error);
    }
  }

  // Get book by ID
  async getBookById(id) {
    try {
      const response = await this.api.get(`/api/books/${id}`);
      return response.data;
    } catch (error) {
      this.handleError(`Error fetching book with ID ${id}`, error);
    }
  }

  // Get book by ISBN
  async getBookByISBN(isbn) {
    try {
      const response = await this.api.get(`/api/books/isbn/${isbn}`);
      return response.data;
    } catch (error) {
      this.handleError(`Error fetching book with ISBN ${isbn}`, error);
    }
  }

  // Search books
  async searchBooks(searchTerm) {
    try {
      const response = await this.api.get('/api/books/search', {
        params: { searchTerm }
      });
      return response.data;
    } catch (error) {
      this.handleError(`Error searching books with term "${searchTerm}"`, error);
    }
  }

  // Get books by genre
  async getBooksByGenre(genre) {
    try {
      const response = await this.api.get(`/api/books/genre/${encodeURIComponent(genre)}`);
      return response.data;
    } catch (error) {
      this.handleError(`Error fetching books by genre "${genre}"`, error);
    }
  }

  // Get available books
  async getAvailableBooks() {
    try {
      const response = await this.api.get('/api/books/available');
      return response.data;
    } catch (error) {
      this.handleError('Error fetching available books', error);
    }
  }

  // Create new book
  async createBook(bookData) {
    try {
      const response = await this.api.post('/api/books', bookData);
      return response.data;
    } catch (error) {
      this.handleError('Error creating book', error);
    }
  }

  // Update book
  async updateBook(id, updateData) {
    try {
      const response = await this.api.put(`/api/books/${id}`, updateData);
      return response.data;
    } catch (error) {
      this.handleError(`Error updating book with ID ${id}`, error);
    }
  }

  // Delete book
  async deleteBook(id) {
    try {
      await this.api.delete(`/api/books/${id}`);
      return true;
    } catch (error) {
      this.handleError(`Error deleting book with ID ${id}`, error);
    }
  }

  // Error handler
  handleError(message, error) {
    console.error(message, error.response?.data || error.message);
    throw error;
  }
}

// Usage Examples
const bookAPI = new BookLibraryAPI();

// Example usage
(async () => {
  try {
    // Get all books
    const allBooks = await bookAPI.getAllBooks();
    console.log('All books:', allBooks);

    // Search for books
    const searchResults = await bookAPI.searchBooks('gatsby');
    console.log('Search results:', searchResults);

    // Create a new book
    const newBook = {
      title: "Example Book",
      author: "Example Author",
      isbn: "9781234567890",
      publishedDate: "2024-01-01T00:00:00Z",
      genre: "Example Genre",
      description: "An example book for testing",
      totalCopies: 2
    };
    const createdBook = await bookAPI.createBook(newBook);
    console.log('Created book:', createdBook);

    // Update the book
    const updateData = { description: "Updated description" };
    const updatedBook = await bookAPI.updateBook(createdBook.id, updateData);
    console.log('Updated book:', updatedBook);

    // Delete the book
    await bookAPI.deleteBook(createdBook.id);
    console.log('Book deleted successfully');

  } catch (error) {
    console.error('API operation failed:', error);
  }
})();
```

---

## Notes

- All datetime fields should be in ISO 8601 format (e.g., "2024-01-01T00:00:00Z")
- ISBN should be unique across all books
- String length validations are enforced server-side
- TotalCopies must be at least 1
- AvailableCopies is automatically calculated and managed by the system
- IsAvailable is automatically determined based on AvailableCopies > 0

For more information about the API implementation, see the source code in the `Controllers/BooksController.cs` file.