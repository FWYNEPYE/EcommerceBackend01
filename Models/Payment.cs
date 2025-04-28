using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentStatus { get; set; }

        public string? TransactionId { get; set; }

        [Required]
        public decimal PaidAmount { get; set; }

        public DateTime? PaidAt { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
