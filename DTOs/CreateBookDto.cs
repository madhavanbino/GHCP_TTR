using System.ComponentModel.DataAnnotations;

namespace BookLibraryApi.DTOs
{
    public class CreateBookDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Author { get; set; } = string.Empty;

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        public DateTime PublishedDate { get; set; }

        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int TotalCopies { get; set; } = 1;
    }
}