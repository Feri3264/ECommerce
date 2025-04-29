using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.OrderItem;
using ECommerce.Domain.Product;
using ECommerce.Domain.Shopcart;
using ECommerce.Domain.ShopcartProductMapper;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Command.AddOrderItemToShopcart;

public class AddOrderItemToShopcartCommandHandler
    (IOrderItemRepository _orderItemRepository,
        IProductRepository _productRepository,
        IShopcartRepository _shopcartRepository,
        IShopcartProductMapperRepository _shopcartProductMapperRepository) : IRequestHandler<AddOrderItemToShopcartCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddOrderItemToShopcartCommand request, CancellationToken cancellationToken)
    {
        var orderItem = await _orderItemRepository
            .GetByShopcartProductAsync(request.shopcartId, request.productId);
        
        var product = await _productRepository.GetByIdAsync(request.productId);
        var shopcart = await _shopcartRepository.GetByIdAsync(request.shopcartId);

        if (orderItem is not null)
        {
            orderItem.AddQuantity();
            shopcart.CalculateTotalPrice(orderItem.Price);
            _shopcartRepository.Update(shopcart);
            _orderItemRepository.Update(orderItem);
            await _orderItemRepository.SaveChangesAsync();
            return Result.Success;
        }
        
        var newOrderItem = new OrderItemModel
            (_productId: product.Id,
                _shopcartId: shopcart.Id,
                _name: product.Name,
                _price: product.Price,
                _quantity: 1,
                _createDate: DateTime.Now,
                _modifiedDate: DateTime.Now);
        await _orderItemRepository.AddAsync(newOrderItem);
        
        //Creating Mapper
        var mapper = new ShopcartProductMapperModel
            (_shopcartId: shopcart.Id,
                _productId: product.Id);
        

        product.AddShopcart(mapper.Id);
        shopcart.AddOrderItem(newOrderItem.Id);
        shopcart.AddProduct(mapper.Id);
        shopcart.CalculateTotalPrice(newOrderItem.Quantity * newOrderItem.Price);
        
        _shopcartRepository.Update(shopcart);
        _productRepository.Update(product);
        await _shopcartProductMapperRepository.AddAsync(mapper);

        await _orderItemRepository.SaveChangesAsync();
        return Result.Success;
        
    }
}