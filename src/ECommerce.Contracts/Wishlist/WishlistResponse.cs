using ECommerce.Contracts.Product;

namespace ECommerce.Contracts.Wishlist;

public record WishlistResponse
    (Guid id,
        List<ProductResponse>? products);