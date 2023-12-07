using CurrencyExchangeService.Core.Models;

namespace CurrencyExchangeService.Core.Interfaces;

public interface IUserRepository
{
    public Task CreateUserAsync(Guid id);

    public Task<bool> IsUserExistsAsync(Guid id);

    public Task DeleteUserAsync(Guid id);

    public Task<List<User>> GetAllUsersAsync();
}