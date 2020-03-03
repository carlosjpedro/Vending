using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vending.Contracts.Interfaces;
using Vending.Contracts.Model;
using Vending.Repositories.Context;

namespace Vending.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly VendingDbContext _dbContext;

        public CurrencyRepository(VendingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CoinStack>> GetValidCurrencies()
        {
            var entities = await _dbContext.Currencies.ToListAsync();
            return entities.Select(x => new CoinStack(x.Value));
        }


        public async Task AddCoins(IEnumerable<CoinStack> coinStacks)
        {
            var savedStacks = await _dbContext
                .Currencies.ToListAsync();

            foreach (var stack in coinStacks.Where(x => x.Count > 0))
            {
                var savedStack = savedStacks.First(x => x.Value == stack.Value);
                {
                    savedStack.Count += stack.Count;
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CoinStack>> GetAvailableCoins()
        {
            var entities = await _dbContext.Currencies.ToListAsync();
            return entities.Select(x => new CoinStack(x.Value, x.Count));
        }

        public async Task RemoveCoins(List<CoinStack> coinStacks)
        {
            var savedStacks = await _dbContext
                .Currencies.ToListAsync();

            foreach (var stack in coinStacks)
            {
                var savedStack = savedStacks.First(x => x.Value == stack.Value);
                {
                    savedStack.Count -= stack.Count;
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}