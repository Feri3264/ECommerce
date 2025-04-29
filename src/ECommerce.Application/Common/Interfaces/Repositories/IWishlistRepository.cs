using System;
using ECommerce.Domain.Wishlist;

namespace ECommerce.Application.Common.Interfaces.Repositories;

public interface IWishlistRepository
{
    Task<WishlistModel> GetByIdAsync(Guid id);
    Task AddAsync(WishlistModel model);
    void Update(WishlistModel model);
    Task SaveChangesAsync();
}
