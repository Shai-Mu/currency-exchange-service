using System.ComponentModel.DataAnnotations;
using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Dto;
using CurrencyExchangeService.Dto.Converters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace CurrencyExchangeService.RestApi.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private const double defaultFee = 0.0005;
        
        private readonly IExchangeService _exchangeService;

        public ExchangeController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        /// <summary>
        /// Совершить обмен валют.
        /// </summary>
        /// <param name="id">Идентификатор пользователя, для которого выполняется обмен.</param>
        /// <param name="exchange"></param>
        /// <response code="200">Операция выполнена.</response>
        /// <response code="400">Ошибка валидации.</response>
        /// <response code="500">Ошибка на стороне сервера.</response>
        [HttpPost]
        [Route("/api/users/{id}/exchange")]
        [SwaggerOperation("MakeExchange")]
        [SwaggerResponse(statusCode: 500, type: typeof(InternalErrorResponse), description: "Ошибка на стороне сервера.")]
        public async Task<IActionResult> MakeExchange([FromRoute][Required]Guid id, [FromBody]Exchange exchange)
        {
            try
            {
                var coreExchangeInfo = ExchangeConverter.Convert(exchange, id, (decimal)defaultFee);

                await _exchangeService.ExecuteExchangeAsync(coreExchangeInfo);

                return Ok();
            }
            catch (InvalidExchangeException e)
            {
                var errors = new ModelStateDictionary();
                errors.AddModelError(nameof(exchange.CurrencyAmountForExchange), e.Message);

                return BadRequest(errors);
            }
            catch (Exception e)
            {
                return StatusCode(500, new InternalErrorResponse(e.ToString()));
            }
        }
    }
}
