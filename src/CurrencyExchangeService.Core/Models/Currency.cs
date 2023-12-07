namespace CurrencyExchangeService.Core.Models;

public class Currency
{
    public Guid Id { get; init; }

    public Currency(Guid id)
    {
        Id = id;
    }
}