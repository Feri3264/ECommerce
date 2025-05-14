using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.RefreshToken;
using ECommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.RefreshToken.Persistence;

public class RefreshTokenRepository
    (ECommerceDbContext _dbContext) : IRefreshTokenRepository
{
    public async Task<RefreshTokenModel> GetRefreshTokenAsync(string refreshToken)
    {
        return await _dbContext.RefreshToken.FirstOrDefaultAsync(r => r.Token == refreshToken);
    }

    public async Task AddAsync(RefreshTokenModel model)
    {
        _dbContext.RefreshToken.Add(model);
    }

    public void Remove(RefreshTokenModel model)
    {
        _dbContext.RefreshToken.Remove(model);
    }
}