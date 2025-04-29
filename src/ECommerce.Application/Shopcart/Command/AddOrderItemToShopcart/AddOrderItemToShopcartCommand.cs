using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Command.AddOrderItemToShopcart;

public record AddOrderItemToShopcartCommand
    (Guid productId , Guid shopcartId) : IRequest<ErrorOr<Success>>;