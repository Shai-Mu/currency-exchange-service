using System.Diagnostics.CodeAnalysis;
using DbCurrency = CurrencyExchangeService.Database.Models.Currency;
using CoreCurrency = CurrencyExchangeService.Core.Models.Currency;

namespace CurrencyExchangeService.Database.Repositories.Converters;

public static class CurrencyConverter
{
    [return: NotNullIfNotNull("dbCurrency")]
    public static CoreCurrency? Convert(DbCurrency? dbCurrency)
    {
        if (dbCurrency is null)
            return null;

        return new CoreCurrency(dbCurrency.Id);
    }
}