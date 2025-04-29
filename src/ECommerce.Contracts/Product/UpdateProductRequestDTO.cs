namespace ECommerce.Contracts.Product;

public record UpdateProductRequestDTO
    (string Name,
        int Price,
        string ShortDesc,
        string FullDesc,
        bool AllowUserComments);