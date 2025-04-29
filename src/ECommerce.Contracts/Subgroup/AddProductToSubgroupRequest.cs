namespace ECommerce.Contracts.Subgroup;

public record AddProductToSubgroupRequest
    (Guid subgroupId , Guid productId);