using ErrorOr;
using MediatR;

namespace ECommerce.Application.Wishlist.Commands.AddProductToWishlist;

public record AddProductToWishlistCommand
(Guid wishlistId , Guid productId) : IRequest<ErrorOr<Success>>;
