namespace CurrencyExchangeService.Core.Exceptions;

public class AccountAlreadyExistsException : Exception
{
    public AccountAlreadyExistsException() : base()
    {
        
    }

    public AccountAlreadyExistsException(string? message) : base(message)
    {
        
    }
}