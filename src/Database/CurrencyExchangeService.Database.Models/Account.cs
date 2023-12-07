namespace CurrencyExchangeService.Database.Models;

public class Account
{
    public Guid UserId { get; set; }
    
    public User? User { get; set; }
    
    public Guid CurrencyId { get; set; }
    
    public Currency? Currency { get; set; }
    
    public decimal Balance { get; set; }

    public Account(Guid userId, Guid currencyId, decimal balance)
    {
        UserId = userId;
        CurrencyId = currencyId;
        Balance = balance;
    }
}