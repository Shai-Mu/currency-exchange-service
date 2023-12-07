namespace CurrencyExchangeService.Core.Models;

public class Exchange
{
    public Guid UserId { get; init; }
    
    public Guid SourceCurrencyId { get; init; }
    
    public Guid TargetCurrencyId { get; init; }
    
    public decimal CurrencyAmountForExchange { get; init; }
    
    public decimal ExchangeRate { get; init; }
    
    public decimal ExchangeFeeRate { get; init; }

    public Exchange(Guid userId,
        Guid sourceCurrencyId,
        Guid targetCurrencyId,
        decimal currencyAmountForExchange,
        decimal exchangeRate,
        decimal exchangeFeeRate)
    {
        UserId = userId;
        SourceCurrencyId = sourceCurrencyId;
        TargetCurrencyId = targetCurrencyId;
        CurrencyAmountForExchange = currencyAmountForExchange;
        ExchangeRate = exchangeRate;
        ExchangeFeeRate = exchangeFeeRate;
    }
}