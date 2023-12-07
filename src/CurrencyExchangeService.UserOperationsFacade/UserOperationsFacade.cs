using CurrencyExchangeService.Core.Interfaces;

namespace CurrencyExchangeService.UserOperationsFacade;

public class UserOperationsFacade : IUserOperationsFacade
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDatabaseCommitter _databaseCommitter;

    public UserOperationsFacade(ICurrencyRepository currencyRepository,
        IAccountRepository accountRepository,
        IUserRepository userRepository,
        IDatabaseCommitter databaseCommitter)
    {
        _currencyRepository = currencyRepository;
        _accountRepository = accountRepository;
        _userRepository = userRepository;
        _databaseCommitter = databaseCommitter;
    }

    public async Task CreateUserAsync(Guid id)
    {
        await _databaseCommitter.BeginTransactionAsync();
        bool rollbackIsNeeded = true;

        try
        {
            await _userRepository.CreateUserAsync(id);
                    
            var currencies = await _currencyRepository.GetAllCurrenciesAsync();
            foreach (var currency in currencies)
            {
                await _accountRepository.CreateAccountAsync(id, currency.Id, 0);
            }
                    
            await _databaseCommitter.CommitTransactionAsync();
            rollbackIsNeeded = false;
        }
        finally
        {
            if (rollbackIsNeeded)
                await _databaseCommitter.RollbackTransactionAsync();
        }
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _databaseCommitter.BeginTransactionAsync();
        bool rollbackIsNeeded = true;

        try
        {
            var currencies = await _currencyRepository.GetAllCurrenciesAsync();
            foreach (var currency in currencies)
            {
                await _accountRepository.DeleteAccountAsync(id, currency.Id);
            }
            
            await _userRepository.DeleteUserAsync(id);

            await _databaseCommitter.CommitTransactionAsync();
            rollbackIsNeeded = false;
        }
        finally
        {
            if (rollbackIsNeeded)
                await _databaseCommitter.RollbackTransactionAsync();
        }
    }
}