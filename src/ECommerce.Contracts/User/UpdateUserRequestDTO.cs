namespace ECommerce.Contracts.User;

public record UpdateUserRequestDTO
    (string name,
        string email,
        string username,
        string password,
        bool isAdmin,
        bool isEditor);