using CoreAccount = CurrencyExchangeService.Core.Models.Account;

namespace CurrencyExchangeService.Dto.Converters;

public static class UserAccountsConverter
{
    public static UserAccounts Convert(Guid userId, List<CoreAccount> accounts)
    {
        return new UserAccounts(userId, accounts.ConvertAll(AccountConverter.Convert));
    }
}