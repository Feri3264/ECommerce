namespace ECommerce.Contracts.Address;

public record PutAddressRequest
    (string Country,
        string City,
        string Street,
        string Alley,
        string Plate);