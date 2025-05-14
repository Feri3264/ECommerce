using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.RefreshToken.Query.GetUserByRefreshToken;

public class GetUserByRefreshTokenQueryHandler
    (IUserRepository _userRepository) : IRequestHandler<GetUserByRefreshTokenQuery, ErrorOr<UserModel>>
{
    public async Task<ErrorOr<UserModel>> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(request.refreshToken);
        if (user is null)
        {
            return Error.NotFound("user.not found" , "User Not Found");
        }

        return user;
    }
}