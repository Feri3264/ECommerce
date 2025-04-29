namespace ECommerce.Contracts.Wishlist;

public record AddProductToWishlistRequest
    (Guid productId, Guid wishlistId);