using System.Data;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeService.Database.Repositories;

public class DatabaseCommitter : IDatabaseCommitter
{
    private readonly DbContext _dbContext;
    private bool _transactionInProgress;

    public DatabaseCommitter(CurrencyExchangeContext dbContext)
    {
        _dbContext = dbContext;
        _transactionInProgress = false;
    }


    public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (_transactionInProgress)
            throw new InvalidOperationException("Transaction already started!");

        await _dbContext.Database.BeginTransactionAsync(isolationLevel);
        
        _transactionInProgress = true;
    }

    public async Task CommitTransactionAsync()
    {
        if (!_transactionInProgress)
            throw new InvalidOperationException("Transaction was not started!");

        await _dbContext.SaveChangesAsync();
        await _dbContext.Database.CommitTransactionAsync();

        _transactionInProgress = false;
    }

    public async Task RollbackTransactionAsync()
    {
        if (!_transactionInProgress)
            throw new InvalidOperationException("Transaction was not started!");
        
        await _dbContext.Database.RollbackTransactionAsync();
        
        _transactionInProgress = false;
    }
}