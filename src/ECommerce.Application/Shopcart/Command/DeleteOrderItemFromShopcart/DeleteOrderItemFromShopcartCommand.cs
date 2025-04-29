using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Command.DeleteOrderItemFromShopcart;

public record DeleteOrderItemFromShopcartCommand
    (Guid productId , Guid shopcartId) : IRequest<ErrorOr<Success>>;