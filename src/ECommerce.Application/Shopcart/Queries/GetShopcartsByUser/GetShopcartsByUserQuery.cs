using ECommerce.Domain.Shopcart;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Queries.GetShopcartsByUser;

public record GetShopcartsByUserQuery
    (Guid userId) : IRequest<ErrorOr<ShopcartModel>>;