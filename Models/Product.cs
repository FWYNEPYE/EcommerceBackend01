using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table("Products")]
    public class Products{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Name {get; set;}

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Description {get; set;}

         [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public decimal Price {get; set;}

         [Required]
        [Column(TypeName = "varchar(100)")]
        public int Stock {get; set;}

         [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string ImageUrl {get; set;}

        [Required]
        [ForeignKey("Category")]
        public int CategoryId {get; set;}


        public Category? Category{get; set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;


    }
}