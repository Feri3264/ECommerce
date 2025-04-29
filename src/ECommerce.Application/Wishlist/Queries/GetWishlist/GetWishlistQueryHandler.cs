using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Product;
using ECommerce.Domain.Wishlist;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Wishlist.Queries.GetWishlist;

public class GetWishlistQueryHandler
(IWishlistRepository _wishlistRepository,
    IWishlistProductMapperReository _wishlistProductMapperReository) : IRequestHandler<GetWishlistQuery, ErrorOr<WishlistModel>>
{
    public async Task<ErrorOr<WishlistModel>> Handle(GetWishlistQuery request, CancellationToken cancellationToken)
    {
        WishlistModel wishlist = await _wishlistRepository.GetByIdAsync(request.id);
        if (wishlist is null)
        {
            return WishlistError.WishlistNotFound;
        }
        
        return wishlist;
    }
}
