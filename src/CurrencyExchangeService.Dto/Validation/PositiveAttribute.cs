using System.ComponentModel.DataAnnotations;

namespace CurrencyExchangeService.Dto.Validation;

public class PositiveAttribute : ValidationAttribute
{
    public PositiveAttribute()
    {
        ErrorMessage = "Value must be positive";
    }
    
    public override bool IsValid(object? value)
    {
        decimal? decimalValue = value as decimal?;

        return decimalValue is null or > decimal.Zero;
    }
}