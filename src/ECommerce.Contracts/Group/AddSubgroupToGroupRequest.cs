namespace ECommerce.Contracts.Group;

public record AddSubgroupToGroupRequest
    (Guid groupId, Guid subgroupId);