namespace ECommerce.Contracts.User;

public record RegisterUserResponse
    (Guid userId,
        string name,
        string email,
        string username,
        string password,
        bool isAdmin,
        bool isEditor,
        DateTime modifiedDate,
        DateTime createDate);