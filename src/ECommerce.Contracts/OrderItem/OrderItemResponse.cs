namespace ECommerce.Contracts.OrderItem;

public record OrderItemResponse
    (Guid shoppingCartId,
        Guid productId,
        string name,
        int price,
        int quantity);