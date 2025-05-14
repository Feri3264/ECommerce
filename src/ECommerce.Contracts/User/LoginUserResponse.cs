namespace ECommerce.Contracts.User;

public record LoginUserResponse
    (Guid userId,
        string name,
        string email,
        string username,
        string password,
        string token,
        string refreshToken,
        bool isAdmin,
        bool isEditor,
        bool isDelete,
        DateTime modifiedDate,
        DateTime createDate);