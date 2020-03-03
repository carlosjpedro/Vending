namespace Vending.Contracts.Exceptions
{
    public class InvalidValue : VendingException
    {
        private const string InvalidValueMessage = "Value cannot be negative.";
        public InvalidValue() : base(InvalidValueMessage)
        { }
    }
}