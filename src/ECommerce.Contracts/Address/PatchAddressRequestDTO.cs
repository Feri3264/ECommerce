namespace ECommerce.Contracts.Address;

public record PatchAddressRequestDTO
    (string? Country,
        string? City,
        string? Street,
        string? Alley,
        string? Plate,
        Guid? UserId);