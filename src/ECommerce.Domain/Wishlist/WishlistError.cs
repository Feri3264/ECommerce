using System;
using ErrorOr;

namespace ECommerce.Domain.Wishlist;

public static class WishlistError
{
    public static Error WishlistProductAlreadyExists = Error.Conflict
    (code : "wishlistProduct.already.exists", description : "WishlistProduct With This Id Already Exists");
    
    public static Error WishlistProductNotFound = Error.NotFound
        (code : "wishlistProduct.not.found", description : "WishlistProduct With This Id Not Found");

    public static Error WishlistNotFound = Error.NotFound
    (code : "wishlist.not.found", description : "Wishlist With This Id Not Found");
}
