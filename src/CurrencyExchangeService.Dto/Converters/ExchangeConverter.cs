using DtoExchange = CurrencyExchangeService.Dto.Exchange;
using CoreExchange = CurrencyExchangeService.Core.Models.Exchange;

namespace CurrencyExchangeService.Dto.Converters;

public static class ExchangeConverter
{
    public static CoreExchange Convert(DtoExchange dtoExchange, Guid userId, decimal defaultExchangeFeeRate)
    {
        decimal exchangeFeeRate = dtoExchange.ExchangeFeeRate ?? defaultExchangeFeeRate;
        
        return new CoreExchange(userId, dtoExchange.SourceCurrencyId, dtoExchange.TargetCurrencyId,
            dtoExchange.CurrencyAmountForExchange, dtoExchange.ExchangeRate, exchangeFeeRate);
    }
}