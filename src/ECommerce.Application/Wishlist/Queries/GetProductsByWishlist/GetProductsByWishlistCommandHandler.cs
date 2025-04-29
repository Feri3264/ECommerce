using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Product;
using ECommerce.Domain.Wishlist;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Wishlist.Queries.GetProductsByWishlist;

public class GetProductsByWishlistCommandHandler
    (IWishlistRepository _wishlistRepository,
        IWishlistProductMapperReository _mapperReository) : IRequestHandler<GetProductsByWishlistCommand , ErrorOr<IEnumerable<ProductModel>>>
{
    public async Task<ErrorOr<IEnumerable<ProductModel>>> Handle(GetProductsByWishlistCommand request, CancellationToken cancellationToken)
    {
        WishlistModel wishlist = await _wishlistRepository.GetByIdAsync(request.wishlistId);
        if (wishlist is null)
        {
            return WishlistError.WishlistNotFound;
        }

        List<ProductModel> products = new List<ProductModel>();
        foreach (var item in wishlist.WishlistProductId)
        {
            products.Add(await _mapperReository.GetProductByMapperIdAsync(item));
        }
        
        return products;
    }
}