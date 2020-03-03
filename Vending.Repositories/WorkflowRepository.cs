using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vending.Contracts.Interfaces;
using Vending.Contracts.Model;
using Vending.Repositories.Context;

namespace Vending.Repositories
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly VendingDbContext _dbContext;

        public WorkflowRepository(VendingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddWorkflowSet(PurchaseWorkflowStep step)
        {
            var entity = new PurchaseWorkflowEntity
            {
                WorkflowId = step.WorkflowId,
                Balance = step.Balance,
                Status = step.Status.ToString(),
                TimeStamp = step.TimeStamp
            };
            await _dbContext.PurchaseWorkflows.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<PurchaseWorkflowStep> GetWorkflowStep(int lastStepId)
        {
            var entity = await _dbContext.PurchaseWorkflows.SingleAsync(x => x.Id == lastStepId);

            return new PurchaseWorkflowStep
            {
                WorkflowId = entity.WorkflowId,
                Balance = entity.Balance,
                Status = (PurchaseStatus)Enum.Parse(typeof(PurchaseStatus), entity.Status),
                TimeStamp = entity.TimeStamp
            };
        }
    }
}