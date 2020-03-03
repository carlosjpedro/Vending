using System.Collections.Generic;

namespace Vending.Models
{
    public class ProductSelectionViewModel
    {
        public int WorkflowStepId { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public decimal Balance { get; set; }
        public string ErrorMessage { get; set; }
    }
}