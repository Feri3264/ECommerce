using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Product;
using ECommerce.Domain.WishlistProductMapper;
using ECommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.WishlistProductMapper.Persistence;

internal class WishlistProductMapperRepository
    (ECommerceDbContext _dbContext) : IWishlistProductMapperReository
{
    public async Task<WishlistProductMapperModel> GetByIdAsync(Guid wishlistId, Guid productId)
    {
        return await _dbContext.WishlistProductMappers
            .FirstOrDefaultAsync(w => w.WishlistId == wishlistId && w.ProductId == productId);
    }

    public async Task<ProductModel> GetProductByMapperIdAsync(Guid mapperId)
    {
        var mapper = await _dbContext.WishlistProductMappers.FirstOrDefaultAsync(w => w.Id == mapperId);
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == mapper.ProductId);
    }

    public async Task AddAsync(WishlistProductMapperModel model)
    {
        await _dbContext.WishlistProductMappers.AddAsync(model);
    }

    public void Delete(WishlistProductMapperModel model)
    {
        _dbContext.WishlistProductMappers.Remove(model);
    }
}