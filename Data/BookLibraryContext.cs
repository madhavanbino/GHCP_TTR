using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Models;

namespace BookLibraryApi.Data
{
    public class BookLibraryContext : DbContext
    {
        public BookLibraryContext(DbContextOptions<BookLibraryContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Book entity
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.ISBN)
                    .IsRequired()
                    .HasMaxLength(13);
                
                entity.HasIndex(e => e.ISBN)
                    .IsUnique();
                
                entity.Property(e => e.Genre)
                    .HasMaxLength(50);
                
                entity.Property(e => e.Description)
                    .HasMaxLength(1000);
                
                entity.Property(e => e.PublishedDate)
                    .IsRequired();
                
                entity.Property(e => e.TotalCopies)
                    .IsRequired()
                    .HasDefaultValue(1);
                
                entity.Property(e => e.AvailableCopies)
                    .IsRequired()
                    .HasDefaultValue(1);
                
                entity.Property(e => e.IsAvailable)
                    .IsRequired()
                    .HasDefaultValue(true);
            });

            // Seed initial data
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    ISBN = "9780743273565",
                    PublishedDate = new DateTime(1925, 4, 10),
                    Genre = "Classic Literature",
                    Description = "A classic American novel set in the Jazz Age",
                    TotalCopies = 5,
                    AvailableCopies = 3,
                    IsAvailable = true
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    ISBN = "9780061120084",
                    PublishedDate = new DateTime(1960, 7, 11),
                    Genre = "Classic Literature",
                    Description = "A gripping tale of racial injustice and childhood innocence",
                    TotalCopies = 3,
                    AvailableCopies = 2,
                    IsAvailable = true
                },
                new Book
                {
                    Id = 3,
                    Title = "1984",
                    Author = "George Orwell",
                    ISBN = "9780451524935",
                    PublishedDate = new DateTime(1949, 6, 8),
                    Genre = "Dystopian Fiction",
                    Description = "A dystopian social science fiction novel",
                    TotalCopies = 4,
                    AvailableCopies = 1,
                    IsAvailable = true
                }
            );
        }
    }
}