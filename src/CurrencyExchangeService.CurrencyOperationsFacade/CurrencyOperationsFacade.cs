using CurrencyExchangeService.Core.Interfaces;

namespace CurrencyExchangeService.CurrencyOperationsFacade;

public class CurrencyOperationsFacade : ICurrencyOperationsFacade
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDatabaseCommitter _databaseCommitter;

    public CurrencyOperationsFacade(IDatabaseCommitter databaseCommitter,
        ICurrencyRepository currencyRepository,
        IAccountRepository accountRepository,
        IUserRepository userRepository)
    {
        _databaseCommitter = databaseCommitter;
        _currencyRepository = currencyRepository;
        _accountRepository = accountRepository;
        _userRepository = userRepository;
    }

    public async Task CreateCurrencyAsync(Guid id)
    {
        await _databaseCommitter.BeginTransactionAsync();
        bool rollbackIsNeeded = true;
                
        try
        {
            await _currencyRepository.CreateCurrencyAsync(id);
                    
            var users = await _userRepository.GetAllUsersAsync();
            foreach (var user in users)
            {
                await _accountRepository.CreateAccountAsync(user.Id, id, 0);
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

    public async Task DeleteCurrencyAsync(Guid id)
    {
        await _databaseCommitter.BeginTransactionAsync();
        bool rollbackIsNeeded = true;
                
        try
        {
            var users = await _userRepository.GetAllUsersAsync();
            foreach (var user in users)
            {
                await _accountRepository.DeleteAccountAsync(user.Id, id);
            }

            await _currencyRepository.DeleteCurrencyAsync(id);

            await _databaseCommitter.CommitTransactionAsync();
            rollbackIsNeeded = false;
        }
        finally
        {
            if (rollbackIsNeeded)
                await _databaseCommitter.BeginTransactionAsync();
        }
    }
}