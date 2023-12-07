using System.Data;

namespace CurrencyExchangeService.Core.Interfaces;

public interface IDatabaseCommitter
{
    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    
    Task CommitTransactionAsync();
    
    Task RollbackTransactionAsync();
}