using System.Security.Cryptography;
using ECommerce.Application.Common.Auth;

namespace ECommerce.Infrastructure.Common.Auth;

public class RefreshTokenService : IRefreshTokenService
{
    public string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        var numbers = RandomNumberGenerator.Create();
        numbers.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}