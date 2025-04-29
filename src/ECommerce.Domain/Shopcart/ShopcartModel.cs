using ECommerce.Domain.Common;
using ErrorOr;

namespace ECommerce.Domain.Shopcart;

public class ShopcartModel : BaseModel
{
    //props
    public int TotalPrice { get; private set; }

    //navigation
    public List<Guid>? OrderItemIds { get; private set; }
    public List<Guid>? ShopcartProductId { get; private set; }
    public Guid? AddressId { get; private set; }
    public Guid UserId { get; private set; }

    //ctor
    public ShopcartModel(Guid _userId,
        int _totalPrice,
        DateTime _createDate,
        DateTime _modifiedDate,
        Guid? id = null) : base(_createDate,_modifiedDate)
    {
        Id = id ?? Guid.NewGuid();
        UserId = _userId;
        TotalPrice = _totalPrice;
    }

    //methods
    public int CalculateTotalPrice(int amount)
    {
        TotalPrice += amount;
        return TotalPrice;
    }
    public ErrorOr<Success> AddOrderItem(Guid id)
    {
        if (OrderItemIds.Contains(id))
        {
            return ShopcartError.OrderItemAlreadyExists;
        }

        OrderItemIds.Add(id);
        return Result.Success;
    }

    public ErrorOr<Success> RemoveOrderItem(Guid id)
    {
        if (OrderItemIds is null || !OrderItemIds.Contains(id))
        {
            return ShopcartError.OrderItemNotFound;
        }
        
        OrderItemIds.Remove(id);
        return Result.Success;
    }

    public ErrorOr<Success> AddProduct(Guid id)
    {
        if (ShopcartProductId.Contains(id))
        {
            return ShopcartError.ShopcartProductAlreadyExists;
        }
        
        ShopcartProductId.Add(id);
        return Result.Success;
    }
    
    public ErrorOr<Success> RemoveProduct(Guid id)
    {
        if (ShopcartProductId is null || !ShopcartProductId.Contains(id))
        {
            return ShopcartError.ShopcartProductNotFound;
        }
        
        ShopcartProductId.Remove(id);
        return Result.Success;
    }
    
    public ErrorOr<Success> AddAddress(Guid id)
    {
        if (AddressId == id)
        {
            return ShopcartError.AddressAlreadyExists;
        }

        AddressId = id;
        return Result.Success;
    }
    
    public ErrorOr<Success> RemoveAddress()
    {
        if (AddressId is null)
        {
            return ShopcartError.AddressNotSet;
        }
        AddressId = null;
        return Result.Success;
    }

    private ShopcartModel()
    { }
}
