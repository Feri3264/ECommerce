using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Shopcart;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Command.DeleteShopcart;

public class DeleteShopcartCommandHandler
    (IUserRepository _userRepository,
        IShopcartRepository _shopcartRepository,
        IOrderItemRepository _orderItemRepository) : IRequestHandler<DeleteShopcartCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteShopcartCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId);
        if (user is null)
        {
            return UserError.UserNotFound;
        }
        
        var shopcart = await _shopcartRepository.GetByIdAsync(request.shopcartId);
        if (shopcart is null)
        {
            return ShopcartError.ShopcartNotFound;
        }

        foreach (var item in shopcart.OrderItemIds)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(item);
            _orderItemRepository.Delete(orderItem);
        }
        
        user.RemoveShopcart();

        _userRepository.Update(user);
        _shopcartRepository.Delete(shopcart);
        await _shopcartRepository.SaveChangesAsync();
        return Result.Success;
    }
}