using System.ComponentModel.DataAnnotations;
using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Dto;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CurrencyExchangeService.RestApi.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserOperationsFacade _userOperationsFacade;

        public UsersController(IUserOperationsFacade userOperationsFacade)
        {
            _userOperationsFacade = userOperationsFacade;
        }

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <response code="200">Пользователь создан.</response>
        /// <response code="409">Пользователь с указанным Id уже существует.</response>
        /// <response code="500">Ошибка на стороне сервера.</response>
        [HttpPost]
        [Route("/api/users/{id}")]
        [SwaggerOperation("CreateUser")]
        [SwaggerResponse(statusCode: 500, type: typeof(InternalErrorResponse), description: "Ошибка на стороне сервера.")]
        public async Task<IActionResult> CreateUser([FromRoute][Required]Guid id)
        {
            try
            {
                await _userOperationsFacade.CreateUserAsync(id);
                
                return Ok();
            }
            catch (UserAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, new InternalErrorResponse(e.ToString()));
            }
        }

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <response code="200">Пользователь удален.</response>
        /// <response code="404">Пользователь с указанным Id не существует.</response>
        /// <response code="500">Ошибка на стороне сервера.</response>
        [HttpDelete]
        [Route("/api/users/{id}")]
        [SwaggerOperation("DeleteUser")]
        [SwaggerResponse(statusCode: 500, type: typeof(InternalErrorResponse), description: "Ошибка на стороне сервера.")]
        public async Task<IActionResult> DeleteUser([FromRoute][Required]Guid id)
        {
            try
            {
                await _userOperationsFacade.DeleteUserAsync(id);

                return Ok();
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
