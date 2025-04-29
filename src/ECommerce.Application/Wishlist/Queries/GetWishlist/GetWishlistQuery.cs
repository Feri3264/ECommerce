using ECommerce.Domain.Wishlist;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Wishlist.Queries.GetWishlist;

public record GetWishlistQuery
(Guid id) : IRequest<ErrorOr<WishlistModel>>;
