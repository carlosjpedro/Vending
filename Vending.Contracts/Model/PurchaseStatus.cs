using System;

namespace Vending.Contracts.Model
{
    public enum PurchaseStatus
    {
        MoneyDeposited,
        Canceled,
        Fulfilled
    }

    public class PurchaseWorkflowStep
    {
        public int Id { get; set; }
        public Guid WorkflowId { get; set; }
        public int Balance { get; set; }
        public int Change { get; set; }
        public int? ProductId { get; set; }
        public DateTime TimeStamp { get; set; }
        public PurchaseStatus Status { get; set; }
    }
}