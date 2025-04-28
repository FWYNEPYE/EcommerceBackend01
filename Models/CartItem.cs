using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class CartItem
    {
        [Key]
        public long CartItemId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("User")]
        public int Id { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }
    }
}
