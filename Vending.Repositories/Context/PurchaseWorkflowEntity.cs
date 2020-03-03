using System;
using System.ComponentModel.DataAnnotations;

namespace Vending.Repositories.Context
{
    public class PurchaseWorkflowEntity
    {
        [Key]
        public int Id { get; set; }
        public int Balance { get; set; }
        public string Status { get; set; }
        public Guid WorkflowId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}