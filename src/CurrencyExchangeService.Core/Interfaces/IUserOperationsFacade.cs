namespace CurrencyExchangeService.Core.Interfaces;

public interface IUserOperationsFacade
{
    public Task CreateUserAsync(Guid id);

    public Task DeleteUserAsync(Guid id);
}