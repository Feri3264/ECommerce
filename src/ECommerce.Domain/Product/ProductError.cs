using System;
using ErrorOr;

namespace ECommerce.Domain.Product;

public static class ProductError
{
    public static Error ProductAlreadyExists = Error.Conflict
    (code : "product.already.exists", description : "Product With This Id Already Exists");

    public static Error ProductNotFound = Error.NotFound
    (code : "product.not.found", description : "Product With This Id Not Found");
    
    public static Error ShopcartProductNotFound = Error.NotFound
        (code : "shopcartProduct.not.found", description : "ShopcartProduct Not Found");
    
    public static Error ShopcartProductAlreadyExists = Error.NotFound
        (code : "shopcartProduct.already.exists", description : "ShopcartProduct Already Exists");
    
    public static Error WishlistNotFound = Error.NotFound
        (code : "wishlist.not.found", description : "Wishlist With This Id Not Found");
    
    public static Error ProductAlreadyExistsInWishlist = Error.NotFound
        (code : "product.exists.in.wishlist", description : "Product Already Exists in Wishlist With This Id");
    
    public static Error RepeatedOrderItem = Error.NotFound
        (code : "repeated.orderItem", description : "OrderItem With This Id Already Assigned To This Product");
    
    public static Error OrderItemNotFound = Error.NotFound
        (code : "orderItem.not.found", description : "OrderItem With This Id Not Found");
}
