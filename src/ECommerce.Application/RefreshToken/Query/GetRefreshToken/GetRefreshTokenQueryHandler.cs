using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.RefreshToken;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.RefreshToken.Query.GetRefreshToken;

public class GetRefreshTokenQueryHandler
    (IRefreshTokenRepository _refreshTokenRepository) : IRequestHandler<GetRefreshTokenQuery , ErrorOr<RefreshTokenModel>>
{
    public async Task<ErrorOr<RefreshTokenModel>> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(request.refreshToken);
        if (refreshToken is null)
        {
            return Error.NotFound("refresh.token.not.found" , "Refresh token not found");
        }
        
        return refreshToken;
    }
}