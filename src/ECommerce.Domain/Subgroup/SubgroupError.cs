using System;
using ErrorOr;

namespace ECommerce.Domain.Subgroup;

public static class SubgroupError
{
    public static Error ProductsAlreadyExists = Error.Conflict
    (code : "product.already.exists" , description : "Product With This Id Already Exsits In This Subgroup");
    
    public static Error ProductNotFound = Error.NotFound
        (code : "product.not.found" , description : "Product With This Id Not Found");

    public static Error SubgroupNotFound = Error.NotFound
    (code : "subgroup.not.found" , description : "Subgroup With This Id Not Found");
}
