using System;
using System.Diagnostics;
using ErrorOr;

namespace ECommerce.Domain.Address;

public static class AddressError
{
    public static Error AddressAlreadyExists = Error.Conflict
    (code :"address.already.exists", description : "Address With This Id Already Exists");
    
    public static Error AddressNotFound = Error.NotFound
        (code :"address.not.found", description : "Address With This Id Not Found");
}
