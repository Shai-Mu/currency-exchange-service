using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using CurrencyExchangeService.Dto.Validation;

namespace CurrencyExchangeService.Dto
{ 
    /// <summary>
    /// Модель, представляющая операцию по обмену валют.
    /// </summary>
    [DataContract]
    public class Exchange
    {
        /// <summary>
        /// Идентификатор валюты, в которой покупается другая валюта.
        /// </summary>
        /// <value>Идентификатор валюты, в которой покупается другая валюта.</value>
        [Required]
        [DataMember(Name="sourceCurrencyId")]
        public Guid SourceCurrencyId { get; set; }

        /// <summary>
        /// Идентификатор покупаемой валюты.
        /// </summary>
        /// <value>Идентификатор покупаемой валюты.</value>
        [Required]
        [DataMember(Name="targetCurrencyId")]
        public Guid TargetCurrencyId { get; set; }

        /// <summary>
        /// Количество исходной валюты, которую необходимо перевести.
        /// </summary>
        /// <value>Количество исходной валюты, которую необходимо перевести.</value>
        [Required]
        [Positive]
        [DataMember(Name="currencyAmountForExchange")]
        public decimal CurrencyAmountForExchange { get; set; }

        /// <summary>
        /// Валютный курс, в котором будет происходить операция обмена. Под курсом понимается количество покупаемой валюты, доступной при покупке на единицу исходной валюты.
        /// </summary>
        /// <value>Валютный курс, в котором будет происходить операция обмена. Под курсом понимается количество покупаемой валюты, доступной при покупке на единицу исходной валюты.</value>
        [Required]
        [Positive]
        [DataMember(Name="exchangeRate")]
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// Процент комиссии.
        /// </summary>
        /// <value>Процент комиссии.</value>
        [IsPercentage]
        [DataMember(Name="exchangeFeeRate")]
        public decimal? ExchangeFeeRate { get; set; }

        public Exchange(Guid sourceCurrencyId,
            Guid targetCurrencyId,
            decimal currencyAmountForExchange,
            decimal exchangeRate,
            decimal? exchangeFeeRate)
        {
            SourceCurrencyId = sourceCurrencyId;
            TargetCurrencyId = targetCurrencyId;
            CurrencyAmountForExchange = currencyAmountForExchange;
            ExchangeRate = exchangeRate;
            ExchangeFeeRate = exchangeFeeRate;
        }
    }
}
