namespace CurrencyExchangeService.Core.Models;

public class Account
{
    public Guid UserId { get; init; }
    
    public Guid CurrencyId { get; set; }
    
    public decimal Balance { get; set; }

    public Account(Guid userId, Guid currencyId, decimal balance)
    {
        UserId = userId;
        CurrencyId = currencyId;
        Balance = balance;
    }
}