using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.ShopcartProductMapper;
using ECommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.ShopcartProductMapper.Persistence;

internal class ShopcartProductMapperRepository
    (ECommerceDbContext _dbContext) : IShopcartProductMapperRepository
{
    public async Task<ShopcartProductMapperModel> GetByIdAsync(Guid shopcartId, Guid productId)
    {
        return await _dbContext.ShopcartProductMappers.FirstOrDefaultAsync(s =>
            s.ShopcartId == shopcartId && s.ProductId == productId);
    }

    public async Task AddAsync(ShopcartProductMapperModel model)
    {
        await _dbContext.ShopcartProductMappers.AddAsync(model);
    }

    public void Delete(ShopcartProductMapperModel model)
    {
        _dbContext.ShopcartProductMappers.Remove(model);
    }
}