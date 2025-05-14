namespace ECommerce.Contracts.RefreshToken;

public record RefreshTokenResponse
    (string JwtToken, string RefreshToken);