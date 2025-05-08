namespace ECommerce.Contracts.User;

public record RegisterUserResponse
    (Guid userId,
        string name,
        string email,
        string username,
        string password,
        bool isAdmin,
        bool isEditor,
        string JwtToken,
        DateTime modifiedDate,
        DateTime createDate);