using ECommerce.Domain.Common;

namespace ECommerce.Domain.WishlistProductMapper;

public class WishlistProductMapperModel : BaseModel
{
    public Guid WishlistId { get; set; }
    public Guid ProductId { get; set; }

    public WishlistProductMapperModel(Guid _wishlistId, Guid _productId, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        WishlistId = _wishlistId;
        ProductId = _productId;
    }

    private WishlistProductMapperModel()
    { }
}