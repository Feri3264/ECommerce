using ECommerce.Contracts.OrderItem;

namespace ECommerce.Contracts.Shopcart;

public record ShopcartResponse
    (Guid userId,
        int totalPrice,
        List<OrderItemResponse>? orderItems = null,
        Guid? addressId = null);