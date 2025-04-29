namespace ECommerce.Contracts.Product;

public record PatchUpdateProductRequestDTO
    (string? Name,
        int? Price,
        string? ShortDesc,
        string? FullDesc,
        bool? AllowUserComments);