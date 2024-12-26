using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCCoreCF.Models
{
    [Table("Category_Name")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public int InputCategoryId { get; set; }
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
