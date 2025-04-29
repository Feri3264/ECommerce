using ECommerce.Domain.Product;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Wishlist.Queries.GetProductsByWishlist;

public record GetProductsByWishlistCommand
    (Guid wishlistId) : IRequest<ErrorOr<IEnumerable<ProductModel>>>;