using System;
using ErrorOr;

namespace ECommerce.Domain.OrderItem;

public class OrderItemError
{
    public static Error OrderItemNotFound = Error.NotFound
        (code: "orderItem.not.found" , description: "OrderItem Not Found");
    
    public static Error OrderItemQunatityIs1 = Error.Validation
        (code: "orderItem.quantity.is.1" , description: "OrderItem Quantity Is 1");
}
