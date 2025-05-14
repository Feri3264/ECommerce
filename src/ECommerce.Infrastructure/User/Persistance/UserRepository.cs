using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.User;
using ECommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.User.Persistance;

internal class UserRepository
    (ECommerceDbContext _dbContext) : IUserRepository
{
    public async Task<IEnumerable<UserModel>> GetUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<UserModel> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserModel> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public async Task<UserModel> LoginValidationAsync(string email, string password)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password ||
                                      u.Username == email && u.Password == password);
    }

    public async Task<bool> IsUsernameExistsAsync(string username)
    {
        return await _dbContext.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<bool> IsEmailExistsAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task AddAsync(UserModel model)
    {
        await _dbContext.Users.AddAsync(model);
    }

    public async Task<bool> AnyAsync(Guid id)
    {
        return await _dbContext.Users.AnyAsync(u => u.Id == id);
    }

    public void Update(UserModel model)
    {
        _dbContext.Users.Update(model);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}