using DtoAccount = CurrencyExchangeService.Dto.Account;
using CoreAccount = CurrencyExchangeService.Core.Models.Account;

namespace CurrencyExchangeService.Dto.Converters;

public static class AccountConverter
{
    public static DtoAccount Convert(CoreAccount coreAccount)
    {
        return new DtoAccount(coreAccount.CurrencyId, coreAccount.Balance);
    }
}