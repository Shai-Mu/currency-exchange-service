namespace CurrencyExchangeService.Database.Models;

public class Currency
{
    public Guid Id { get; set; }
    
    public Currency(Guid id)
    {
        Id = id;
    }
}