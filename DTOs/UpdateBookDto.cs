using System.ComponentModel.DataAnnotations;

namespace BookLibraryApi.DTOs
{
    public class UpdateBookDto
    {
        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(100)]
        public string? Author { get; set; }

        [StringLength(13)]
        public string? ISBN { get; set; }

        public DateTime? PublishedDate { get; set; }

        [StringLength(50)]
        public string? Genre { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Range(1, int.MaxValue)]
        public int? TotalCopies { get; set; }

        public bool? IsAvailable { get; set; }
    }
}