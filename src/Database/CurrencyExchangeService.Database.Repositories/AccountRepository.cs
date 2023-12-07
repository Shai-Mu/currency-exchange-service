using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Database.Context;
using CurrencyExchangeService.Database.Models;
using CurrencyExchangeService.Database.Repositories.Converters;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeService.Database.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly CurrencyExchangeContext _dbContext;

    public AccountRepository(CurrencyExchangeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAccountAsync(Guid userId, Guid currencyId, decimal balance)
    {
        var account = await _dbContext.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.UserId == userId && a.CurrencyId == currencyId);

        if (account is not null)
            throw new AccountAlreadyExistsException(
                $"Account for user with id {userId} and currency with id {currencyId} already exists.");

        var newAccount = new Account(userId, currencyId, balance);

        await _dbContext.Accounts.AddAsync(newAccount);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAccountBalanceAsync(Guid userId, Guid currencyId, decimal balance)
    {
        var account = await _dbContext.Accounts
            .FirstOrDefaultAsync(a => a.UserId == userId && a.CurrencyId == currencyId);

        if (account is  null)
            throw new AccountNotFoundException(
                $"Account for user with id {userId} and currency with id {currencyId} was not found.");

        account.Balance = balance;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAccountAsync(Guid userId, Guid currencyId)
    {
        var account = await _dbContext.Accounts
            .FirstOrDefaultAsync(a => a.UserId == userId && a.CurrencyId == currencyId);

        if (account is  null)
            throw new AccountNotFoundException(
                $"Account for user with id {userId} and currency with id {currencyId} was not found.");

        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Core.Models.Account>> GetAccountsForUserAsync(Guid userId)
    {
        var accounts = await _dbContext.Accounts
            .Where(a => a.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        return accounts.ConvertAll(AccountConverter.Convert)!;
    }

    public async Task<Core.Models.Account> GetAccountAsync(Guid userId, Guid currencyId)
    {
        var account = await _dbContext.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.UserId == userId && a.CurrencyId == currencyId);

        if (account is  null)
            throw new AccountNotFoundException(
                $"Account for user with id {userId} and currency with id {currencyId} was not found.");

        return AccountConverter.Convert(account);
    }
}