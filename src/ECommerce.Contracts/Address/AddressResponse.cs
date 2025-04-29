namespace ECommerce.Contracts.Address;

public record AddressResponse
    (Guid addressId,
        Guid userId,
        string country,
        string city,
        string? street,
        string? alley,
        string plate);