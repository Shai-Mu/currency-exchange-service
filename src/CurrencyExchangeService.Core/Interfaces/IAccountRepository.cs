using CurrencyExchangeService.Core.Models;

namespace CurrencyExchangeService.Core.Interfaces;

public interface IAccountRepository
{
    public Task CreateAccountAsync(Guid userId, Guid currencyId, decimal balance);

    public Task UpdateAccountBalanceAsync(Guid userId, Guid currencyId, decimal balance);

    public Task DeleteAccountAsync(Guid userId, Guid currencyId);

    public Task<List<Account>> GetAccountsForUserAsync(Guid userId);

    public Task<Account> GetAccountAsync(Guid userId, Guid currencyId);
}