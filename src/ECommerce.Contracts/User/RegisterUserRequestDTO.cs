namespace ECommerce.Contracts.User;

public record RegisterUserRequestDTO
    (string name,
        string email,
        string username,
        string password,
        bool isAdmin,
        bool isEditor);