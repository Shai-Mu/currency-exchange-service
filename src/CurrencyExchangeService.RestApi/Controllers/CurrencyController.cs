using System.ComponentModel.DataAnnotations;
using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace CurrencyExchangeService.RestApi.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyOperationsFacade _currencyOperationsFacade;

        public CurrencyController(ICurrencyOperationsFacade currencyOperationsFacade)
        {
            _currencyOperationsFacade = currencyOperationsFacade;

        }

        /// <summary>
        /// Создать валюту.
        /// </summary>
        /// <param name="id">Идентификатор валюты.</param>
        /// <response code="200">Валюта создана.</response>
        /// <response code="409">Валюта с указанным Id уже существует.</response>
        /// <response code="500">Ошибка на стороне сервера.</response>
        [HttpPost]
        [Route("/api/currency/{id}")]
        [SwaggerOperation("CreateCurrency")]
        [SwaggerResponse(statusCode: 500, type: typeof(InternalErrorResponse), description: "Ошибка на стороне сервера.")]
        public async Task<IActionResult> CreateCurrency([FromRoute][Required]Guid id)
        {
            try
            {
                await _currencyOperationsFacade.CreateCurrencyAsync(id);

                return Ok();
            }
            catch (CurrencyAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, new InternalErrorResponse(e.ToString()));
            }
        }

        /// <summary>
        /// Удалить валюту.
        /// </summary>
        /// <param name="id">Идентификатор валюты.</param>
        /// <response code="200">Валюта удалена.</response>
        /// <response code="404">Валюта с указанным Id не существует.</response>
        /// <response code="500">Ошибка на стороне сервера.</response>
        [HttpDelete]
        [Route("/api/currency/{id}")]
        [SwaggerOperation("DeleteCurrency")]
        [SwaggerResponse(statusCode: 500, type: typeof(InternalErrorResponse), description: "Ошибка на стороне сервера.")]
        public async Task<IActionResult> DeleteCurrency([FromRoute][Required]Guid id)
        { 
            try
            {
                await _currencyOperationsFacade.DeleteCurrencyAsync(id);

                return Ok();
            }
            catch (CurrencyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, new InternalErrorResponse(e.ToString()));
            }
        }
    }
}
