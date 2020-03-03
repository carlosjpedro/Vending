using System.Threading.Tasks;
using Vending.Contracts.Model;

namespace Vending.Contracts.Interfaces
{
    public interface IWorkflowRepository
    {
        Task<int> AddWorkflowSet(PurchaseWorkflowStep step);
        Task<PurchaseWorkflowStep> GetWorkflowStep(int lastStepId);
    }
}