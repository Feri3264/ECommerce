using ECommerce.Application.Address.Queries.GetAddress;
using ECommerce.Application.Shopcart.Command.AddAddressToShopcart;
using ECommerce.Application.Shopcart.Command.AddOrderItemToShopcart;
using ECommerce.Application.Shopcart.Command.CreateShopcart;
using ECommerce.Application.Shopcart.Command.DeleteAddressFromShopcart;
using ECommerce.Application.Shopcart.Command.DeleteShopcart;
using ECommerce.Application.Shopcart.Command.DeleteOrderItemFromShopcart;
using ECommerce.Application.Shopcart.Queries.GetOrderItemsByShopcart;
using ECommerce.Application.Shopcart.Queries.GetShopcart;
using ECommerce.Application.Shopcart.Queries.GetShopcartsByUser;
using ECommerce.Contracts.OrderItem;
using ECommerce.Contracts.Shopcart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/[controller]/[action]")]
public class ShopcartController
    (IMediator _mediator) : ApiController
{
    
    #region GetShopcart

    [HttpGet]
    public async Task<IActionResult> GetShopcart([FromQuery] Guid id)
    {
        var exitingShopcart = new GetShopcartQuery(id);
        var shopcartResult = await _mediator.Send(exitingShopcart);

        var exitingOrderItems = new GetOrderItemsByShopcartQuery(id);
        var orderItemModels = await _mediator.Send(exitingOrderItems);
        
        List<OrderItemResponse> orderItems = new List<OrderItemResponse>();
        foreach (var orderItemModel in orderItemModels)
        {
            orderItems.Add(new OrderItemResponse(
                shoppingCartId: id,
                productId: orderItemModel.ProductId,
                name: orderItemModel.Name,
                price: orderItemModel.Price,
                quantity: orderItemModel.Quantity));
        }

        var response = new ShopcartResponse(
            userId: shopcartResult.Value.UserId,
            totalPrice: shopcartResult.Value.TotalPrice,
            orderItems: orderItems);
        
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetShopcartByUser([FromQuery] Guid userId)
    {
        var exitingShopcart = new GetShopcartsByUserQuery(userId);
        var shopcartResult = await _mediator.Send(exitingShopcart);

        var exitingOrderItems = new GetOrderItemsByShopcartQuery(shopcartResult.Value.Id);
        var orderItemModels = await _mediator.Send(exitingOrderItems);
        
        List<OrderItemResponse> orderItems = new List<OrderItemResponse>();
        foreach (var orderItemModel in orderItemModels)
        {
            orderItems.Add(new OrderItemResponse(
                shoppingCartId: shopcartResult.Value.Id,
                productId: orderItemModel.ProductId,
                name: orderItemModel.Name,
                price: orderItemModel.Price,
                quantity: orderItemModel.Quantity));
        }

        var response = new ShopcartResponse(
            userId: shopcartResult.Value.UserId,
            totalPrice: shopcartResult.Value.TotalPrice,
            orderItems: orderItems);
        
        return Ok(response);
    }

    #endregion

    #region CreateShopcart

    [HttpPost]
    public async Task<IActionResult> CreateShopCart([FromBody] CreateShopcartRequest request)
    {
        var command = new CreateShopcartCommand(request.userId);
        var createShopcartResult = await _mediator.Send(command);

        return createShopcartResult.Match<IActionResult>(shopcart => CreatedAtAction(nameof(GetShopcart),
                new { id = shopcart.Id }, new ShopcartResponse
                (userId: shopcart.UserId,
                    totalPrice : shopcart.TotalPrice)),
            Problem);
    }

    #endregion

    #region DeleteShopcart

    [HttpDelete("{userId}/{shopcartId}")]
    public async Task<IActionResult> DeleteShopCart([FromRoute] Guid userId , [FromRoute] Guid shopcartId)
    {
        var command = new DeleteShopcartCommand(userId , shopcartId);
        var deleteShopcartResult = await _mediator.Send(command);

        return deleteShopcartResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }

    #endregion

    #region OrderItemActions

    [HttpPost]
    public async Task<IActionResult> AddOrderItemToShopcart([FromBody] AddOrderItemToShopcartRequest request)
    {
        var command = new AddOrderItemToShopcartCommand(request.productId, request.shopcartId);
        var addOrderItemResult = await _mediator.Send(command);

        return addOrderItemResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }
    
    [HttpDelete("{productId}/{shopcartId}")]
    public async Task<IActionResult> DeleteOrderItemFromShopcart([FromRoute] Guid productId , [FromRoute] Guid shopcartId)
    {
        var command = new DeleteOrderItemFromShopcartCommand(productId, shopcartId);
        var deleteOrderItemResult = await _mediator.Send(command);

        return deleteOrderItemResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }

    #endregion

    #region AddressActions

    [HttpPatch]
    public async Task<IActionResult> AddAddressToShopcart([FromQuery] Guid shopcartId, [FromQuery] Guid addressId)
    {
        var command = new AddAddressToShopcartCommand(shopcartId, addressId);
        var response = await _mediator.Send(command);

        return response.Match(_ => Ok(), Problem);
    }

    [HttpPatch]
    public async Task<IActionResult> DeleteAddressFromShopcart([FromQuery] Guid shopcartId)
    {
        var command = new DeleteAddressFromShopcartCommand(shopcartId);
        var response = await _mediator.Send(command);
        
        return response.Match(_ => Ok(), Problem);
    }
    #endregion
}