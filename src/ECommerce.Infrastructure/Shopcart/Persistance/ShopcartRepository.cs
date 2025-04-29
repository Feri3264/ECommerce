using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Shopcart;
using ECommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Shopcart.Persistance;

internal class ShopcartRepository
    (ECommerceDbContext _dbContext) : IShopcartRepository
{
    public async Task<ShopcartModel> GetByIdAsync(Guid Id)
    {
        return await _dbContext.Shopcarts.FirstOrDefaultAsync(s => s.Id == Id);
    }

    public async Task<ShopcartModel> GetByUserIdAsync(Guid userId)
    {
        return await _dbContext.Shopcarts.FirstOrDefaultAsync(s => s.UserId == userId);
    }

    public async Task AddAsync(ShopcartModel model)
    {
        await _dbContext.Shopcarts.AddAsync(model);
    }

    public void Delete(ShopcartModel model)
    {
        _dbContext.Shopcarts.Remove(model);
    }

    public void Update(ShopcartModel model)
    {
        _dbContext.Shopcarts.Update(model);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}