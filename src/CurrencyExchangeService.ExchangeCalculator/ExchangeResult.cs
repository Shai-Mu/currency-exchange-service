namespace CurrencyExchangeService.ExchangeCalculator;

public class ExchangeResult
{
    public decimal SourceBalanceValue { get; set; }
    
    public decimal TargetBalanceValue { get; set; }

    public ExchangeResult(decimal sourceBalanceValue, decimal targetBalanceValue)
    {
        SourceBalanceValue = sourceBalanceValue;
        TargetBalanceValue = targetBalanceValue;
    }
}