namespace CurrencyExchangeService.Core.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException() : base()
    {
        
    }

    public AccountNotFoundException(string? message) : base(message)
    {
        
    }
}