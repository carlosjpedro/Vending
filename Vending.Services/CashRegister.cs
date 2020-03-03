using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vending.Contracts.Interfaces;
using Vending.Contracts.Model;

namespace Vending.Services
{
    public class CashRegister : ICashRegister
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CashRegister(ICurrencyRepository currencyRepository) => _currencyRepository = currencyRepository;

        public Task<IEnumerable<CoinStack>> AvailableCurrencies() => _currencyRepository.GetValidCurrencies();
        public Task AddCoins(IEnumerable<CoinStack> coinStacks) => _currencyRepository.AddCoins(coinStacks);

        public async Task<IEnumerable<CoinStack>> GetCoins(int value)
        {
            if (value <= 0) return Enumerable.Empty<CoinStack>();

            var remainingValue = value;

            var coinStacks = await _currencyRepository.GetAvailableCoins();
            var orderedStacks = coinStacks.OrderByDescending(x => x.Value);
            var change = new List<CoinStack>();

            foreach (var stack in orderedStacks)
            {
                var coinCount = Math.DivRem(remainingValue, stack.Value, out var tempRemainder);

                if (coinCount > stack.Count)
                {
                    coinCount = stack.Count;
                    tempRemainder = remainingValue - coinCount * stack.Value;
                }

                remainingValue = tempRemainder;

                change.Add(new CoinStack(stack.Value, coinCount));

                if (remainingValue == 0) break;
            }

            await _currencyRepository.RemoveCoins(change);

            return change;
        }
    }
}