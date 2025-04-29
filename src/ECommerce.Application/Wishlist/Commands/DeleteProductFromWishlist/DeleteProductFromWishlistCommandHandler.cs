using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Wishlist;
using ECommerce.Domain.Product;
using ECommerce.Domain.WishlistProductMapper;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Wishlist.Commands.DeleteProductFromWishlist;

public class DeleteProductFromWishlistCommandHandler
(IWishlistRepository _wishlistRepository,
IProductRepository _productRepository,
IWishlistProductMapperReository _wishlistProductMapperReository) : IRequestHandler<DeleteProductFromWishlistCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteProductFromWishlistCommand request, CancellationToken cancellationToken)
    {
        WishlistModel wishlist = await _wishlistRepository.GetByIdAsync(request.wishlistId);
        ProductModel product = await _productRepository.GetByIdAsync(request.productId);
        WishlistProductMapperModel mapper = await _wishlistProductMapperReository.GetByIdAsync(wishlist.Id , product.Id);
        
        if (wishlist is null)
        {
            return WishlistError.WishlistNotFound;
        }    

        if (! await _productRepository.AnyAsync(request.productId))
        {
            return ProductError.ProductNotFound;
        }
        
        product.RemoveWishlist(mapper.Id);
        wishlist.RemoveProduct(mapper.Id);
        
        _wishlistRepository.Update(wishlist);
        _productRepository.Update(product);
        _wishlistProductMapperReository.Delete(mapper);
        await _wishlistRepository.SaveChangesAsync();
        return Result.Success;
    }
}
