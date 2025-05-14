using ECommerce.Application.Common.Auth;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.RefreshToken;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.RefreshToken.Command.GenerateRefreshToken;

public class GenerateRefreshTokenCommandHandler
    (IRefreshTokenService _refreshTokenService,
        IRefreshTokenRepository _refreshTokenRepository,
        IUserRepository _userRepository) : IRequestHandler<GenerateRefreshTokenCommand , string>
{
    public async Task<string> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (request.user.RefreshToken is not null)
        {
            var tokenModel = await _refreshTokenRepository.GetRefreshTokenAsync(request.user.RefreshToken);
            _refreshTokenRepository.Remove(tokenModel);
        }

        var newRefreshToken = _refreshTokenService.GenerateRefreshToken();
        var newTokenModel = new RefreshTokenModel(
            _token: newRefreshToken,
            _expireTime: DateTime.UtcNow.AddDays(7),
            _userId: request.user.Id);
        
        request.user.SetRefreshToken(newRefreshToken);

        await _refreshTokenRepository.AddAsync(newTokenModel);
        _userRepository.Update(request.user);
        await _userRepository.SaveChangesAsync();
        return newRefreshToken;
    }
}