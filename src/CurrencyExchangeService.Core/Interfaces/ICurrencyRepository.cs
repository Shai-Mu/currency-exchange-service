using CurrencyExchangeService.Core.Models;

namespace CurrencyExchangeService.Core.Interfaces;

public interface ICurrencyRepository
{
    public Task CreateCurrencyAsync(Guid id);

    public Task DeleteCurrencyAsync(Guid id);

    public Task<List<Currency>> GetAllCurrenciesAsync();
}