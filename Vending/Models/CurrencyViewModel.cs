using System.ComponentModel.DataAnnotations;

namespace Vending.Models
{
    public class CurrencyViewModel
    {
        public const string CountValidationMessage = "Value must a positive integer.";
        public int Value { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = CountValidationMessage)]
        public int Count { get; set; }
    }
}