namespace ECommerce.Contracts.Address;

public record CreateAddressRequest
    (Guid userId,
        string country,
        string city,
        string street,
        string alley,
        string plate);