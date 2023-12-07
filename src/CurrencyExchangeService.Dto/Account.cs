using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CurrencyExchangeService.Dto
{ 
    /// <summary>
    /// Модель пользовательского счёта в определённой валюте.
    /// </summary>
    [DataContract]
    public class Account
    {

        /// <summary>
        /// Идентификатор валюты, в которой был открыт счёт.
        /// </summary>
        /// <value>Идентификатор валюты, в которой был открыт счёт.</value>
        [Required]
        [DataMember(Name="currencyId")]
        public Guid CurrencyId { get; private set; }

        /// <summary>
        /// Количество валюты, находящейся на данном счёте.
        /// </summary>
        /// <value>Количество валюты, находящейся на данном счёте.</value>
        [Required]
        [DataMember(Name="balance")]
        public decimal Balance { get; set; }

        public Account(Guid currencyId, 
            decimal balance)
        {
            CurrencyId = currencyId;
            Balance = balance;
        }
    }
}
