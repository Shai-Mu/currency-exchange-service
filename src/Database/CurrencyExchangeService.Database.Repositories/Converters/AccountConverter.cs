using System.Diagnostics.CodeAnalysis;
using DbAccount = CurrencyExchangeService.Database.Models.Account;
using CoreAccount = CurrencyExchangeService.Core.Models.Account;

namespace CurrencyExchangeService.Database.Repositories.Converters;

public static class AccountConverter
{
    [return: NotNullIfNotNull("dbAccount")]
    public static CoreAccount? Convert(DbAccount? dbAccount)
    {
        if (dbAccount is null)
            return null;

        return new CoreAccount(dbAccount.UserId, dbAccount.CurrencyId, dbAccount.Balance);
    }
}