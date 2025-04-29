using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Command.DeleteShopcart;

public record DeleteShopcartCommand
    (Guid userId , Guid shopcartId) : IRequest<ErrorOr<Success>>;