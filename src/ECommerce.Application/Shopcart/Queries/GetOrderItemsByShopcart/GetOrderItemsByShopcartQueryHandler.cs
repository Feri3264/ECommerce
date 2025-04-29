using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.OrderItem;
using ECommerce.Domain.Shopcart;
using MediatR;

namespace ECommerce.Application.Shopcart.Queries.GetOrderItemsByShopcart;

public class GetOrderItemsByShopcartQueryHandler
    (IShopcartRepository _shopcartRepository,
        IOrderItemRepository _orderItemRepository) : IRequestHandler<GetOrderItemsByShopcartQuery, IEnumerable<OrderItemModel>>
{
    public async Task<IEnumerable<OrderItemModel>> Handle(GetOrderItemsByShopcartQuery request, CancellationToken cancellationToken)
    {
        var shopcart = await _shopcartRepository.GetByIdAsync(request.shopcartId);
        
        var orderItems = new List<OrderItemModel>();
        foreach (var item in shopcart.OrderItemIds)
        {
            orderItems.Add(await _orderItemRepository.GetByIdAsync(item));
        }
        
        return orderItems;
    }
}