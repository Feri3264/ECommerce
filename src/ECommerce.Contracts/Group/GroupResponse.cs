using ECommerce.Contracts.Subgroup;

namespace ECommerce.Contracts.Group;

public record GroupResponse
    (Guid groupId,
        string name,
        List<SubgroupResponse>? Subgroups = null);