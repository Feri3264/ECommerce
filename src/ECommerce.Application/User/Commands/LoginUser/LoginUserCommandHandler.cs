using ECommerce.Application.Common.Auth;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Commands.LoginUser;

public class LoginUserCommandHandler
    (IUserRepository _userRepository,
        IRefreshTokenRepository _refreshTokenRepository,
        IRefreshTokenService _refreshTokenService) : IRequestHandler<LoginUserCommand , ErrorOr<UserModel>>
{
    public async Task<ErrorOr<UserModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.LoginValidationAsync(request.emailOrUsername , request.password);
        if (user is null)
        {
            return UserError.LoginValidationFailed;
        }
        
        return user;
    }
}