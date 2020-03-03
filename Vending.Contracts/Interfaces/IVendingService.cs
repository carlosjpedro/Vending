using System.Collections.Generic;
using System.Threading.Tasks;
using Vending.Contracts.Model;

namespace Vending.Contracts.Interfaces
{
    public interface IVendingService
    {
        Task<int> DepositCurrency(IEnumerable<CoinStack> coinStacks);
        Task<IEnumerable<Product>> AvailableProducts();
        Task<IEnumerable<CoinStack>> PurchaseProduct(int workflowStepId, int productId);
        Task<decimal> GetBalance(int lastStepId);
        Task<IEnumerable<CoinStack>> Cancel(int lastStepId);
    }
}
