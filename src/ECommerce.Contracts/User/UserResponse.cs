namespace ECommerce.Contracts.User;

public record UserResponse
    (Guid userId,
        string name,
        string email,
        string username,
        string password,
        string refreshToken,
        bool isAdmin,
        bool isEditor,
        bool isDelete,
        DateTime modifiedDate,
        DateTime createDate);