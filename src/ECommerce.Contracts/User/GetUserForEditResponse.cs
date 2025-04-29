namespace ECommerce.Contracts.User;

public record GetUserForEditResponse
    (string name,
        string email,
        string username);