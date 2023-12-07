using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Database.Context;
using CurrencyExchangeService.Database.Models;
using CurrencyExchangeService.Database.Repositories.Converters;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeService.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CurrencyExchangeContext _dbContext;

    public UserRepository(CurrencyExchangeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateUserAsync(Guid id)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user is not null)
            throw new UserAlreadyExistsException($"User with id {id} already exists.");

        var newUser = new User(id);

        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();
    }

    public Task<bool> IsUserExistsAsync(Guid id)
    {
        return _dbContext.Users
            .AnyAsync(u => u.Id == id);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
            throw new UserNotFoundException($"User with id {id} was not found.");

        _dbContext.Remove(user);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Core.Models.User>> GetAllUsersAsync()
    {
        var users = await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();

        return users.ConvertAll(UserConverter.Convert)!;
    }
}