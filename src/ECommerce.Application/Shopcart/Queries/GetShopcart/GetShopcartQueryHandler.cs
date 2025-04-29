using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Application.Shopcart.Queries.GetOrderItemsByShopcart;
using ECommerce.Domain.OrderItem;
using ECommerce.Domain.Shopcart;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Queries.GetShopcart;

public class GetShopcartQueryHandler
    (IShopcartRepository _shopcartRepository,
        IOrderItemRepository _orderItemRepository) : IRequestHandler<GetShopcartQuery, ErrorOr<ShopcartModel>>
{
    public async Task<ErrorOr<ShopcartModel>> Handle(GetShopcartQuery request, CancellationToken cancellationToken)
    {
        var shopcart = await _shopcartRepository.GetByIdAsync(request.shopcartId);
        if (shopcart is null)
        {
            return ShopcartError.ShopcartNotFound;
        }

        return shopcart;
    }
}