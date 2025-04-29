using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Application.Product.Command.CreateProduct;
using ECommerce.Domain.Shopcart;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Command.CreateShopcart;

public class CreateShopcartCommandHandler
    (IUserRepository _userRepository,
        IShopcartRepository _shopcartRepository) : IRequestHandler<CreateShopcartCommand, ErrorOr<ShopcartModel>>
{
    public async Task<ErrorOr<ShopcartModel>> Handle(CreateShopcartCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId);
        if (user is null)
        {
            return UserError.UserNotFound;
        }

        var newShopcart = new ShopcartModel
            (_userId: request.userId,
                _createDate: DateTime.Now,
                _modifiedDate: DateTime.Now,
                _totalPrice: 0);

        user.AddShopcart(newShopcart.Id);
        
        _userRepository.Update(user);
        await _shopcartRepository.AddAsync(newShopcart);
        await _shopcartRepository.SaveChangesAsync();
        return newShopcart;
    }
}