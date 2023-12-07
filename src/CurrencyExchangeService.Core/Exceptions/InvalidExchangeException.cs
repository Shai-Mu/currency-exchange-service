namespace CurrencyExchangeService.Core.Exceptions;

public class InvalidExchangeException : Exception
{
    public InvalidExchangeException() : base()
    {
        
    }

    public InvalidExchangeException(string? message) : base(message)
    {
        
    }
}