using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table("Categories")]
    public class Category{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name {get; set;}

        [Required]
        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Description {get; set;}


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;


    }
}