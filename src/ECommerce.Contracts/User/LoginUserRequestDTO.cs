namespace ECommerce.Contracts.User;

public record LoginUserRequestDTO
    (string emailOrUsername, string password);