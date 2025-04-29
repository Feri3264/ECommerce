using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Product;
using ECommerce.Domain.Wishlist;
using ECommerce.Domain.WishlistProductMapper;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Wishlist.Commands.AddProductToWishlist;

public class AddProductToWishlistCommandHandler
(IWishlistRepository _wishlistRepository,
IProductRepository _productRepository,
IWishlistProductMapperReository _wishlistProductMapperReository) : IRequestHandler<AddProductToWishlistCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddProductToWishlistCommand request, CancellationToken cancellationToken)
    {        
        WishlistModel wishlist = await _wishlistRepository.GetByIdAsync(request.wishlistId);
        ProductModel product = await _productRepository.GetByIdAsync(request.productId);
        
        if (wishlist is null)
        {
            return WishlistError.WishlistNotFound;
        }    

        if (product is null)
        {
            return ProductError.ProductNotFound;
        }
        
        WishlistProductMapperModel mapper = new
            (_wishlistId: wishlist.Id,
                _productId: product.Id);

        product.AddWishlist(mapper.Id);
        wishlist.AddProduct(mapper.Id);
        
        _wishlistRepository.Update(wishlist);
        _productRepository.Update(product);
        await _wishlistProductMapperReository.AddAsync(mapper);
        await _wishlistRepository.SaveChangesAsync();
        return Result.Success;
    }
}
