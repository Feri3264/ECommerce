using System;
using ErrorOr;

namespace ECommerce.Domain.Shopcart;

public static class ShopcartError
{
    public static Error ShopcartNotFound = Error.NotFound
        (code : "shopcart.not.found", description : "Shopcart With This Id Not Found");
    
    public static Error ShopcartProductAlreadyExists = Error.Conflict
        (code : "shopcartProduct.already.exists", description : "ShopcartProduct Already Exists");
    
    public static Error ShopcartProductNotFound = Error.NotFound
        (code : "shopcartProduct.not.found", description : "ShopcartProduct Not Found");

    public static Error OrderItemAlreadyExists = Error.Conflict
    (code : "orderItem.already.exists", description : "OrderItem With This Id Already Exists In This Shopcart");
    
    public static Error OrderItemNotFound = Error.NotFound
        (code : "orderItem.not.found", description : "OrderItem With This Id Not Found");

    public static Error AddressAlreadyExists = Error.Conflict
    (code : "Address.already.exists", description : "Address With This Id Already Assigned For This Shopcart");
    
    public static Error AddressNotSet = Error.Conflict
        (code : "address.not.set", description : "Address Has Not Been Set");
    
    public static Error AddressNotFound = Error.NotFound
        (code : "address.not.found", description : "Address With This Id Not Found");
}
