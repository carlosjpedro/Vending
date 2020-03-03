using System.ComponentModel.DataAnnotations;

namespace Vending.Repositories.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Portions { get; set; }
    }
}