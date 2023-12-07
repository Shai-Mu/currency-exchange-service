using CurrencyExchangeService.Core.Models;

namespace CurrencyExchangeService.Core.Interfaces;

public interface IExchangeService
{
    public Task ExecuteExchangeAsync(Exchange exchangeInfo);

}