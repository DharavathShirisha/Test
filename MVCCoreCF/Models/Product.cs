using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCCoreCF.Models
{
    [Table("Products")]
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        public int IPNumber { get; set; }
        public string ProductName { get; set; }

        public int Match { get; set; }

        public ICollection<Category> Category { get; set; }
    }
}
