using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Wishlist;
using ECommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Wishlist.Persistance;

internal class WishlistRepository
    (ECommerceDbContext _dbContext) : IWishlistRepository
{
    public async Task<WishlistModel> GetByIdAsync(Guid id)
    {
        return await _dbContext.Wishlists.FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task AddAsync(WishlistModel model)
    {
        await _dbContext.Wishlists.AddAsync(model);
    }

    public void Update(WishlistModel model)
    {
        _dbContext.Update(model);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}