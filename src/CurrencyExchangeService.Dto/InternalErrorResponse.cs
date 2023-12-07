using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace CurrencyExchangeService.Dto
{ 
    /// <summary>
    /// Модель ошибки сервиса, возникшей при выполнении операций.
    /// </summary>
    [DataContract]
    public class InternalErrorResponse
    {
        /// <summary>
        /// Текстовое описание ошибки.
        /// </summary>
        /// <value>Текстовое описание ошибки.</value>
        [Required]
        [DataMember(Name="errorMessage")]
        public string ErrorMessage { get; set; }

        public InternalErrorResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
