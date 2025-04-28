using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("User")]
        public int Id { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }
    }
}
