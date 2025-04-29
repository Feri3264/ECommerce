using ECommerce.Domain.Shopcart;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Queries.GetShopcart;

public record GetShopcartQuery
    (Guid shopcartId) : IRequest<ErrorOr<ShopcartModel>>;