namespace CurrencyExchangeService.Database.Models;

public class User
{
    public Guid Id { get; set; }
    
    public ICollection<Account>? Accounts { get; set; }

    public User(Guid id)
    {
        Id = id;
    }
}