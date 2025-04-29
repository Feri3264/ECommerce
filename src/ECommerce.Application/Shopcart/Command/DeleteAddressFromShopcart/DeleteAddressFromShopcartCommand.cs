using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Command.DeleteAddressFromShopcart;

public record DeleteAddressFromShopcartCommand
    (Guid shopCartId) : IRequest<ErrorOr<Success>>;