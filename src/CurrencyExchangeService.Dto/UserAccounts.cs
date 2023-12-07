using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace CurrencyExchangeService.Dto
{ 
    /// <summary>
    /// Модель списка счётов пользователя.
    /// </summary>
    [DataContract]
    public class UserAccounts
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        /// <value>Идентификатор пользователя.</value>
        [Required]
        [DataMember(Name="userId")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Массив счетов пользователя.
        /// </summary>
        /// <value>Массив счётов пользователя.</value>
        [Required]
        [DataMember(Name="accounts")]
        public List<Account> Accounts { get; set; }

        public UserAccounts(Guid userId, 
            List<Account> accounts)
        {
            UserId = userId;
            Accounts = accounts;
        }
    }
}
