namespace ECommerce.Contracts.User;

public record ChangePasswordRequestDTO
    (string newPassword , string oldPassword);