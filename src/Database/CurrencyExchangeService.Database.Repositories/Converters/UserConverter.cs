using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using CurrencyExchangeService.Core.Models;
using DbUser = CurrencyExchangeService.Database.Models.User;
using CoreUser = CurrencyExchangeService.Core.Models.User;

namespace CurrencyExchangeService.Database.Repositories.Converters;

public static class UserConverter
{
    [return: NotNullIfNotNull("dbUser")]
    public static CoreUser? Convert(DbUser? dbUser)
    {
        if (dbUser is null)
            return null;

        return new CoreUser(dbUser.Id);
    }
}