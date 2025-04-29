namespace ECommerce.Contracts.Product;

public record CreateProductRequestDTO
    (string name,
        int price,
        string shortDesc,
        string fullDesc,
        bool allowUserComment);