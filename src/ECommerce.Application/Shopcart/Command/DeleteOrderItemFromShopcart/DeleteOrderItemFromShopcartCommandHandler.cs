using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.OrderItem;
using ECommerce.Domain.Product;
using ECommerce.Domain.Shopcart;
using ECommerce.Domain.ShopcartProductMapper;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Command.DeleteOrderItemFromShopcart;

public class DeleteOrderItemFromShopcartCommandHandler
    (IOrderItemRepository _orderItemRepository,
        IProductRepository _productRepository,
        IShopcartRepository _shopcartRepository,
        IShopcartProductMapperRepository _shopcartProductMapperRepository) : IRequestHandler<DeleteOrderItemFromShopcartCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteOrderItemFromShopcartCommand request, CancellationToken cancellationToken)
    {
        var orderItem = await _orderItemRepository
            .GetByShopcartProductAsync(request.shopcartId, request.productId);
        
        var product = await _productRepository.GetByIdAsync(request.productId);
        var shopcart = await _shopcartRepository.GetByIdAsync(request.shopcartId);
        var
            mapper = await _shopcartProductMapperRepository.GetByIdAsync(shopcart.Id, product.Id);

        if (orderItem is null)
        {
            return OrderItemError.OrderItemNotFound;
        }

        if (orderItem.Quantity > 1)
        {
            orderItem.RemoveQuantity();
            shopcart.CalculateTotalPrice(-orderItem.Price);
            _shopcartRepository.Update(shopcart);
            _orderItemRepository.Update(orderItem);
            await _orderItemRepository.SaveChangesAsync();
            return Result.Success;
        }
        
        _orderItemRepository.Delete(orderItem);
        

        product.RemoveShopcart(mapper.Id);
        shopcart.RemoveOrderItem(orderItem.Id);
        shopcart.RemoveProduct(mapper.Id);
        
        _productRepository.Update(product);
        _shopcartRepository.Update(shopcart);
        _shopcartProductMapperRepository.Delete(mapper);
        
        await _orderItemRepository.SaveChangesAsync();
        return Result.Success;

    }
}