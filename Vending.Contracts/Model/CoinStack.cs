using Vending.Contracts.Exceptions;

namespace Vending.Contracts.Model
{
    public class CoinStack
    {
        public CoinStack(int value, int count = 0)
        {
            if (value <= 0)
            {
                throw new InvalidValue();
            }

            if (count < 0)
            {
                throw new InvalidCurrencyAmount();
            }

            Value = value;
            Count = count;
        }

        public int Value { get; }
        public int Count { get; }
    }
}
