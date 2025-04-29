namespace ECommerce.Contracts.User;

public record PatchUpdateUserRequestDTO
    (string? name,
        string? email,
        string? username,
        string? password,
        bool? isAdmin,
        bool? isEditor);