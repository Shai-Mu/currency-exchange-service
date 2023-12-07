using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models;

namespace CurrencyExchangeService.ExchangeCalculator;

public class ExchangeService : IExchangeService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IDatabaseCommitter _databaseCommitter;


    public ExchangeService(IAccountRepository accountRepository,
        IDatabaseCommitter databaseCommitter)
    {
        _accountRepository = accountRepository;
        _databaseCommitter = databaseCommitter;
    }

    public async Task ExecuteExchangeAsync(Exchange exchangeInfo)
    {
        await _databaseCommitter.BeginTransactionAsync();
        bool rollbackIsNeeded = true;

        try
        {
            var sourceAccount = await _accountRepository.GetAccountAsync(exchangeInfo.UserId, exchangeInfo.SourceCurrencyId);
            var targetAccount = await _accountRepository.GetAccountAsync(exchangeInfo.UserId, exchangeInfo.TargetCurrencyId);
            
            var exchangeResult = CalculateExchange(sourceAccount.Balance,
                targetAccount.Balance,
                exchangeInfo.CurrencyAmountForExchange,
                exchangeInfo.ExchangeRate,
                exchangeInfo.ExchangeFeeRate);
            
            EnsureOperationIsValid(exchangeResult);

            await _accountRepository.UpdateAccountBalanceAsync(exchangeInfo.UserId, exchangeInfo.SourceCurrencyId,
                exchangeResult.SourceBalanceValue);

            await _accountRepository.UpdateAccountBalanceAsync(exchangeInfo.UserId, exchangeInfo.TargetCurrencyId,
                exchangeResult.TargetBalanceValue);

            await _databaseCommitter.CommitTransactionAsync();
            rollbackIsNeeded = false;
        }
        finally
        {
            if (rollbackIsNeeded)
                await _databaseCommitter.RollbackTransactionAsync();
        }
    }
    
    private ExchangeResult CalculateExchange(decimal currentSourceBalance,
        decimal currentTargetBalance,
        decimal transferCurrencyAmount,
        decimal exchangeRate,
        decimal feeRate)
    {
        decimal exchangeResultWithoutFee = transferCurrencyAmount * exchangeRate;

        decimal feeToTake = feeRate * exchangeResultWithoutFee;

        decimal addToTargetBalance = exchangeResultWithoutFee - feeToTake;

        decimal resultSourceBalance = currentSourceBalance - transferCurrencyAmount; 
        decimal resultTargetBalance = currentTargetBalance + addToTargetBalance;

        return new ExchangeResult(resultSourceBalance, resultTargetBalance);
    }

    private void EnsureOperationIsValid(ExchangeResult exchangeResult)
    {
        if (exchangeResult.SourceBalanceValue < 0)
            throw new InvalidExchangeException("Source balance does not has enough currency.");
    }

}