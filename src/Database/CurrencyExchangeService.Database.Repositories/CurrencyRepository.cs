using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Database.Context;
using CurrencyExchangeService.Database.Models;
using CurrencyExchangeService.Database.Repositories.Converters;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeService.Database.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly CurrencyExchangeContext _dbContext;

    public CurrencyRepository(CurrencyExchangeContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateCurrencyAsync(Guid id)
    {
        var currency = await _dbContext.Currencies
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (currency is not null)
            throw new CurrencyAlreadyExistsException($"Currency with id {id} already exists.");

        var newCurrency = new Currency(id);

        await _dbContext.Currencies.AddAsync(newCurrency);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCurrencyAsync(Guid id)
    {
        var currency = await _dbContext.Currencies
            .FirstOrDefaultAsync(u => u.Id == id);

        if (currency is null)
            throw new CurrencyNotFoundException($"Currency with id {id} was not found.");

        _dbContext.Remove(currency);

        await _dbContext.SaveChangesAsync();
        
    }

    public async Task<List<Core.Models.Currency>> GetAllCurrenciesAsync()
    {
        var currencies = await _dbContext.Currencies
            .AsNoTracking()
            .ToListAsync();

        return currencies.ConvertAll(CurrencyConverter.Convert)!;
    }
}