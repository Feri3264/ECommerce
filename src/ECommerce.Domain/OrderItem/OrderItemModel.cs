using System;
using ECommerce.Domain.Common;
using ErrorOr;

namespace ECommerce.Domain.OrderItem;

public class OrderItemModel : BaseModel
{
    //props
    public string Name { get; private set; }
    public int Price { get; private set; }
    public int Quantity { get; private set; }

    public int TotalOrderPrice { get; private set; }

    //navigation
    public Guid ProductId { get; private set; }
    public Guid ShopcartId { get; private set; }

    //ctor
    public OrderItemModel(Guid _productId,
        Guid _shopcartId,
        string _name,
        int _price,
        int _quantity,
        DateTime _createDate,
        DateTime _modifiedDate,
        Guid? id = null) : base(_createDate , _modifiedDate)
    {
        Id = id ?? Guid.NewGuid();
        ProductId = _productId;
        ShopcartId = _shopcartId;
        Name = _name;
        Price = _price;
        Quantity = _quantity;
        TotalOrderPrice = _price;
    }

    //methods
    public int CalculateTotalOrderPrice()
    {
        TotalOrderPrice = Price * Quantity;
        return TotalOrderPrice;
    }
    public ErrorOr<Success> AddQuantity()
    {
        Quantity++;
        return Result.Success;
    }
    
    public ErrorOr<Success> RemoveQuantity()
    {
        if (Quantity == 0)
        {
            return OrderItemError.OrderItemQunatityIs1;
        }
        
        Quantity--;
        return Result.Success;
    }

    private OrderItemModel()
    { }
}
