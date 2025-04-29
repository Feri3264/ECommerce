namespace ECommerce.Contracts.User;

public record UserResponse
    (Guid userId,
        string name,
        string email,
        string username,
        string password,
        bool isAdmin,
        bool isEditor,
        bool isDelete,
        DateTime modifiedDate,
        DateTime createDate);