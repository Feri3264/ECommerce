using System.Runtime.InteropServices.JavaScript;
using ECommerce.Domain.Common;
using ErrorOr;

namespace ECommerce.Domain.Product;

public class ProductModel : BaseModel
{
    //props
    public string Name { get; private set; }
    public int Price { get; private set; }
    public string ShortDesc { get; private set; }
    public string FullDesc { get; private set; }
    public bool AllowUserComments { get; private set; }
    public bool IsDelete {get; private set;}

    //navigation
    public List<Guid>? ShopcartProductId = new();
    
    public List<Guid>? WishlistProductId = new();
    public Guid SubgroupId { get; private set; }


    //ctor
    public ProductModel(
        string _name,
        int _price,
        string _shortDesc,
        string _fullDesc,
        bool _allowUserComments,
        bool _isDelete,
        DateTime _createDate,
        DateTime _modifiedDate,
        Guid _subgroupId,
        Guid? id = null) : base(_createDate,_modifiedDate)
    {
        Id = id ?? Guid.NewGuid();
        Name = _name;
        Price = _price;
        ShortDesc = _shortDesc;
        FullDesc = _fullDesc;
        AllowUserComments = _allowUserComments;
        IsDelete = _isDelete;        
        SubgroupId = _subgroupId;
    }

    //methods
    public void ChangeSubgroup(Guid id)
    {
        SubgroupId = id;
    }

    public void UnsetSubgroup()
    {
        SubgroupId = Guid.Parse("d859e766-fb34-4914-9c8d-27b02c40ffd4");
    }

    public ErrorOr<Success> AddShopcart(Guid id)
    {
        if (ShopcartProductId.Contains(id))
        {
            return ProductError.ShopcartProductAlreadyExists;
        }
        
        ShopcartProductId.Add(id);
        return Result.Success;
    }
    
    public ErrorOr<Success> RemoveShopcart(Guid id)
    {
        if (ShopcartProductId is null || !ShopcartProductId.Contains(id))
        {
            return ProductError.ShopcartProductNotFound;
        }
        
        ShopcartProductId.Remove(id);
        return Result.Success;
    }

    public ErrorOr<Success> AddWishlist(Guid id)
    {
        if (WishlistProductId.Contains(id))
        {
            return ProductError.ProductAlreadyExistsInWishlist;
        }
        
        WishlistProductId.Add(id);
        return Result.Success;
    }
    
    public ErrorOr<Success> RemoveWishlist(Guid id)
    {
        if (WishlistProductId is null || !WishlistProductId.Contains(id))
        {
            return ProductError.WishlistNotFound;
        }
        
        WishlistProductId.Remove(id);
        return Result.Success;
    }

    public void DeleteProduct()
    {
        IsDelete = !IsDelete;
    }

    private ProductModel()
    { }

    #region Setters

    public ErrorOr<Success> ChangeName(string? name)
    {
        if (name is null)
            return Error.Validation();
        
        Name = name ?? Name;
        return Result.Success;
    }
    
    public ErrorOr<Success> ChangePrice(int? price)
    {
        if (price is null)
            return Error.Validation();
        
        Price = price ?? Price;
        return Result.Success;
    }
    
    public ErrorOr<Success> ChangeShortDesc(string? shortDesc)
    {
        if (shortDesc is null)
            return Error.Validation();
        
        ShortDesc = shortDesc ?? ShortDesc;
        return Result.Success;
    }
    
    public ErrorOr<Success> ChangeFullDesc(string? fullDesc)
    {
        if (fullDesc is null)
            return Error.Validation();
        
        FullDesc = fullDesc ?? FullDesc;
        return Result.Success;
    }
    
    public ErrorOr<Success> ChangeAllowComments(bool? allowComments)
    {
        if (allowComments is null)
            return Error.Validation();
        
        AllowUserComments = allowComments ?? AllowUserComments;
        return Result.Success;
    }

    public void ChangeModifiedDate(DateTime? modifiedDate)
    {
        ModifiedDate = modifiedDate ?? ModifiedDate;
    }
    #endregion
}
