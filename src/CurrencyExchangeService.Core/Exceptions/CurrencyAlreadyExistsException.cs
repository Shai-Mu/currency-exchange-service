namespace CurrencyExchangeService.Core.Exceptions;

public class CurrencyAlreadyExistsException : Exception
{
    public CurrencyAlreadyExistsException() : base()
    {
        
    }

    public CurrencyAlreadyExistsException(string? message) : base(message)
    {
        
    }
}