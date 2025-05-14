using ECommerce.Domain.RefreshToken;
using ECommerce.Domain.User;

namespace ECommerce.Application.Common.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshTokenModel> GetRefreshTokenAsync(string refreshToken);
    Task AddAsync(RefreshTokenModel model);
    void Remove(RefreshTokenModel model);
}