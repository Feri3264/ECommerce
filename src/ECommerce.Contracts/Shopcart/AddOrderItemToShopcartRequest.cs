namespace ECommerce.Contracts.Shopcart;

public record AddOrderItemToShopcartRequest
    (Guid productId, Guid shopcartId);