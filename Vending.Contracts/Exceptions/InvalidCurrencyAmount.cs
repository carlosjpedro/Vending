namespace Vending.Contracts.Exceptions
{
    public class InvalidCurrencyAmount : VendingException
    {
        private const string InvalidAmountMessage = "CoinStack amount cannot be a negative integer.";
        public InvalidCurrencyAmount() : base(InvalidAmountMessage) 
        { }
    }
}