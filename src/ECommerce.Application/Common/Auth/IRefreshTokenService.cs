namespace ECommerce.Application.Common.Auth;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();
}