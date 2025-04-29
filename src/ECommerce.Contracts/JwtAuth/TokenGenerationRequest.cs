namespace ECommerce.Contracts.JwtAuth;

public record TokenGenerationRequest
    (Guid userId,
        string email,
        string password);