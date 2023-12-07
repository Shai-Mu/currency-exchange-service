namespace CurrencyExchangeService.Core.Interfaces;

public interface ICurrencyOperationsFacade
{
    public Task CreateCurrencyAsync(Guid id);

    public Task DeleteCurrencyAsync(Guid id);
}