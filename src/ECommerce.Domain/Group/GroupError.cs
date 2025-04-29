using System;
using ErrorOr;

namespace ECommerce.Domain.Group;

public static class GroupError
{
    public static Error SubgroupAlreadyExists = Error.Conflict
    (code : "subgroup.already.exists" , description : "Subgroup With This Id Already Exists In This Group");
    
    public static Error SubgroupNotFound = Error.NotFound
        (code : "subgroup.not.found" , description : "Subgroup With This Id Not Found In This Group");

    public static Error GroupNotFound = Error.NotFound
    (code : "group.not.found" , description : "Group With This Id Not Found");
}
