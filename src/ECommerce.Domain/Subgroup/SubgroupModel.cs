using System;
using ECommerce.Domain.Common;
using ErrorOr;

namespace ECommerce.Domain.Subgroup;

public class SubgroupModel : BaseModel
{
    //props
    public string Name { get; private set; }
    public bool IsDelete { get; private set; }

    //navigation
    public Guid GroupId { get; private set; }
    public List<Guid>? ProductIds = new();

    //ctor
    public SubgroupModel(string _name,
        Guid _groupId,
        DateTime _createDate,
        DateTime _modifiedDate,
        Guid? id = null) : base(_createDate , _modifiedDate)
    {
        Id = id ?? Guid.NewGuid();
        Name = _name;
        GroupId = _groupId;
    }
    
    //methods
    public void ChangeName(string name)
    {
        Name = name;
    }
    
    public ErrorOr<Success> AddProduct(Guid id)
    {
        if (ProductIds.Contains(id))
        {
            return SubgroupError.ProductsAlreadyExists;
        }

        ProductIds.Add(id);
        return Result.Success;
    }


    public ErrorOr<Success> RemoveProduct(Guid id)
    {
        if (ProductIds is null || !ProductIds.Contains(id))
        {
            return SubgroupError.ProductNotFound;
        }
        
        ProductIds.Remove(id);
        return Result.Success;
    }

    public ErrorOr<Success> ChangeGroup(Guid id)
    {
        GroupId = id;
        return Result.Success;
    }
    
    public ErrorOr<Success> UnsetGroup()
    {
        GroupId = Guid.Parse("2a308c81-0485-49a2-8822-a8d61a981093");
        return Result.Success;
    }

    public void DeleteSubgroup()
    {
        IsDelete = !IsDelete;
    }
        
    private SubgroupModel()
    { }
}
