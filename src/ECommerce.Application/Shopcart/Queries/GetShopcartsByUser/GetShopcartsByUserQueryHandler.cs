using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Shopcart;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Queries.GetShopcartsByUser;

public class GetShopcartsByUserQueryHandler
    (IUserRepository _userRepository,
        IShopcartRepository _shopcartRepository) : IRequestHandler<GetShopcartsByUserQuery, ErrorOr<ShopcartModel>>
{
    public async Task<ErrorOr<ShopcartModel>> Handle(GetShopcartsByUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId);
        if (user is null)
        {
            return UserError.UserNotFound;
        }
        
        var shopcart = await _shopcartRepository.GetByUserIdAsync(request.userId);
        if (shopcart is null)
        {
            return ShopcartError.ShopcartNotFound;
        }
        
        return shopcart;
    }
}