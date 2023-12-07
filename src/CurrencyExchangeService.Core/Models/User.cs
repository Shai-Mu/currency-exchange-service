using System.Collections.Immutable;

namespace CurrencyExchangeService.Core.Models;

public class User
{
    public Guid Id { get; init; }
    

    public User(Guid id)
    {
        Id = id;
    }
}