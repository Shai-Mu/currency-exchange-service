namespace CurrencyExchangeService.Core.Exceptions;

public class CurrencyNotFoundException : Exception
{
    public CurrencyNotFoundException() : base()
    {
        
    }

    public CurrencyNotFoundException(string? message) : base(message)
    {
        
    }
}