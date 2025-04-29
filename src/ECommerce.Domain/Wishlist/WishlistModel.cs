using System;
using ECommerce.Domain.Common;
using ErrorOr;

namespace ECommerce.Domain.Wishlist;

public class WishlistModel : BaseModel
{
    //props
    public bool IsDelete { get; private set; }
    
    //navigations
    public List<Guid>? WishlistProductId { get; private set; }
    public Guid UserId { get; private set; }

    //ctor
    public WishlistModel(Guid _userId,
        DateTime _createDate,
        DateTime _modifiedDate,
        Guid? id = null) : base(_createDate , _modifiedDate)
    {
        Id = id ?? Guid.NewGuid();
        UserId = _userId;
    }

    //methods
    public ErrorOr<Success> AddProduct(Guid id)
    {
        if (WishlistProductId.Contains(id))
        {
            return WishlistError.WishlistProductAlreadyExists;
        }

        WishlistProductId.Add(id);
        return Result.Success;
    }

    public ErrorOr<Success> RemoveProduct(Guid id)
    {
        if (WishlistProductId is null || !WishlistProductId.Contains(id))
        {
            return WishlistError.WishlistProductNotFound;
        }
        
        WishlistProductId.Remove(id);
        return Result.Success;
    }

    public void DeleteWishlist()
    {
        IsDelete = !IsDelete;
    }

    private WishlistModel()
    { }
}
