using System.ComponentModel.DataAnnotations;

namespace CurrencyExchangeService.Dto.Validation;

public class IsPercentageAttribute : ValidationAttribute
{
    public IsPercentageAttribute()
    {
        ErrorMessage = "Value must be between 0 and 1";
    }
    
    public override bool IsValid(object? value)
    {
        decimal? decimalValue = value as decimal?;

        return decimalValue is null or >= decimal.Zero and <= decimal.One;
    }
}