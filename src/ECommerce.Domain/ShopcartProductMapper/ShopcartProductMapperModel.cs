using ECommerce.Domain.Common;

namespace ECommerce.Domain.ShopcartProductMapper;

public class ShopcartProductMapperModel : BaseModel
{
    public Guid ShopcartId { get; set; }
    public Guid ProductId { get; set; }

    public ShopcartProductMapperModel(Guid _shopcartId, Guid _productId , Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        ShopcartId = _shopcartId;
        ProductId = _productId;
    }

    private ShopcartProductMapperModel()
    { }
}