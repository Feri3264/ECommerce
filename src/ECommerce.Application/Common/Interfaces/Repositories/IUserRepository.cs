using System;
using ECommerce.Domain.User;

namespace ECommerce.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserModel>> GetUsersAsync();
    Task<UserModel> GetByIdAsync(Guid id);
    Task<UserModel> GetByRefreshTokenAsync(string refreshToken);
    Task<UserModel> LoginValidationAsync(string emailOrusername , string password);
    Task<bool> IsUsernameExistsAsync(string username);
    Task<bool> IsEmailExistsAsync(string username);
    Task AddAsync(UserModel model);
    Task<bool> AnyAsync(Guid id);
    void Update(UserModel model);
    Task SaveChangesAsync();
}
