using System.Collections.Generic;
using System.Threading.Tasks;
using Vending.Contracts.Model;

namespace Vending.Contracts.Interfaces
{
    public interface ICashRegister
    {
        Task<IEnumerable<CoinStack>> AvailableCurrencies();
        Task AddCoins(IEnumerable<CoinStack> coinStacks);
        Task<IEnumerable<CoinStack>> GetCoins(int remainingValue);
    }
}