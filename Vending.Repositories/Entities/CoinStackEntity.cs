using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vending.Repositories.Entities
{
    public class CoinStackEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Value { get; set; }
        [Required]
        public int Count { get; set; }
    }
}