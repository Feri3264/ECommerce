using System;
using ECommerce.Domain.Common;
using ErrorOr;

namespace ECommerce.Domain.Group;

public class GroupModel : BaseModel
{
    //props
    public string Name { get; private set; }
    public bool IsDelete { get; private set; }

    //navigation
    public List<Guid>? SubgroupIds { get; private set; }

    //ctor
    public GroupModel(string _name,
        DateTime _createDate,
        DateTime _modifiedDate,
        Guid? id = null) : base(_createDate,_modifiedDate)
    {
        Id = id ?? Guid.NewGuid();
        Name = _name;
    }

    //methods
    public void ChangeName(string? name)
    {
        Name = name ?? Name;
    }
    
    public ErrorOr<Success> AddSubgroup(Guid id)
    {
        if (SubgroupIds == null)
        {
            SubgroupIds.Add(id);
            return Result.Success;
        }
        
        if (SubgroupIds.Contains(id))
        {
            return GroupError.SubgroupAlreadyExists;
        }
        
        SubgroupIds.Add(id);
        return Result.Success;
    }

    public ErrorOr<Success> RemoveSubgroup(Guid id)
    {
        if (SubgroupIds == null || !SubgroupIds.Contains(id))
        {
            return GroupError.SubgroupNotFound;
        }
        
        SubgroupIds.Remove(id);
        return Result.Success;
    }

    public void DeleteGroup()
    {
        IsDelete = !IsDelete;
    }
    
    private GroupModel()
    { }
}
