using ECommerce.Domain.Product;
using ECommerce.Domain.WishlistProductMapper;

namespace ECommerce.Application.Common.Interfaces.Repositories;

public interface IWishlistProductMapperReository
{
    Task<WishlistProductMapperModel> GetByIdAsync(Guid wishlistId , Guid productId);
    Task<ProductModel> GetProductByMapperIdAsync(Guid mapperId);
    Task AddAsync(WishlistProductMapperModel model);
    void Delete(WishlistProductMapperModel model);
}