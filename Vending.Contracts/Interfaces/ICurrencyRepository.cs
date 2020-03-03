using System.Collections.Generic;
using System.Threading.Tasks;
using Vending.Contracts.Model;

namespace Vending.Contracts.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<CoinStack>> GetValidCurrencies();
        Task AddCoins(IEnumerable<CoinStack> coinStack);
        Task<IEnumerable<CoinStack>> GetAvailableCoins();
        Task RemoveCoins(List<CoinStack> change);
    }
}
