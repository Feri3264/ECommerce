using ECommerce.Domain.OrderItem;
using MediatR;

namespace ECommerce.Application.Shopcart.Queries.GetOrderItemsByShopcart;

public record GetOrderItemsByShopcartQuery
    (Guid shopcartId) : IRequest<IEnumerable<OrderItemModel>>;