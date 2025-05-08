namespace ECommerce.Application.Common.Auth;

public interface IJwtGenerator
{
    public string GenerateJwtToken(Guid userId, string email, string password , bool isAdmin = false, bool isEditor = false);
}