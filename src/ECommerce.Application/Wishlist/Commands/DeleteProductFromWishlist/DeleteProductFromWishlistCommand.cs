using ErrorOr;
using MediatR;

namespace ECommerce.Application.Wishlist.Commands.DeleteProductFromWishlist;

public record DeleteProductFromWishlistCommand
(Guid wishlistId , Guid productId) : IRequest<ErrorOr<Success>>;
