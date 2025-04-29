using ECommerce.Domain.Shopcart;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Command.CreateShopcart;

public record CreateShopcartCommand
    (Guid userId) : IRequest<ErrorOr<ShopcartModel>>;