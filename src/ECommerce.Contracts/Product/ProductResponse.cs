using ECommerce.Contracts.Subgroup;

namespace ECommerce.Contracts.Product;

public record ProductResponse
    (Guid productId,
        string name,
        int  price,
        string shortDesc,
        string fullDesc,
        bool allowUserComments,
        bool isDelete,
        DateTime createDate,
        DateTime modifiedDate,
        SubgroupResponse? subgroupId = null);