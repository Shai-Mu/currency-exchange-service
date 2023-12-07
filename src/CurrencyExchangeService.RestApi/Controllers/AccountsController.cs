using System.ComponentModel.DataAnnotations;
using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Dto;
using CurrencyExchangeService.Dto.Converters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace CurrencyExchangeService.RestApi.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public AccountsController(IAccountRepository accountRepository, 
            IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Изменить количество валюты на счёте.
        /// </summary>
        /// <param name="currencyId">Идентификатор валюты, для которой выполняется изменение.</param>
        /// <param name="account"></param>
        /// <param name="userId">Идентификатор пользователя, для которого выполняется изменение.</param>
        /// <response code="200">Обновление выполнено успешно.</response>
        /// <response code="404">Пользователь с указанным Id не существует.</response>
        /// <response code="500">Ошибка на стороне сервера.</response>
        [HttpPut]
        [Route("/api/users/{userId}/accounts/{currencyId}")]
        [SwaggerOperation("EditUserAccountBalance")]
        [SwaggerResponse(statusCode: 500, type: typeof(InternalErrorResponse), description: "Ошибка на стороне сервера.")]
        public async Task<IActionResult> EditUserAccountBalance([FromRoute][Required]Guid userId, [FromRoute][Required]Guid currencyId, [FromBody]Account account)
        {
            try
            {
                await _accountRepository.UpdateAccountBalanceAsync(userId, currencyId, account.Balance);
                
                return Ok();
            }
            catch (AccountNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, new InternalErrorResponse(e.ToString()));
            }
        }

        /// <summary>
        /// Получить счета пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <response code="200">Пользователь получен.</response>
        /// <response code="404">Пользователь с указанным Id не существует.</response>
        /// <response code="500">Ошибка на стороне сервера.</response>
        [HttpGet]
        [Route("/api/users/{id}/accounts")]
        [SwaggerOperation("GetUserAccounts")]
        [SwaggerResponse(statusCode: 200, type: typeof(UserAccounts), description: "Пользователь получен.")]
        [SwaggerResponse(statusCode: 500, type: typeof(InternalErrorResponse), description: "Ошибка на стороне сервера.")]
        public async Task<IActionResult> GetUserAccounts([FromRoute][Required]Guid id)
        {
            try
            {
                if (!await _userRepository.IsUserExistsAsync(id))
                {
                    throw new UserNotFoundException($"User with id {id} was not found.");
                }

                var accounts = await _accountRepository.GetAccountsForUserAsync(id);

                return Ok(UserAccountsConverter.Convert(id, accounts));
            }
            catch (UserNotFoundException e)
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
