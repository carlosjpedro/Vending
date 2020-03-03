using System;

namespace Vending.Contracts.Exceptions
{
    public abstract class VendingException : Exception
    {
        protected VendingException(string message) : base(message) { }
    }

    public class InsufficientFounds : VendingException
    {
        private const string InsufficientFoundsMessage = "Insufficient amount.";
        public InsufficientFounds() : base(InsufficientFoundsMessage) { }
    }
}
