using System;

namespace ECommerce.Domain.Common;

public class BaseModel
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; protected set; }
    public DateTime ModifiedDate { get; protected set; }


    public BaseModel(DateTime _createDate , DateTime _modifiedDate)
    {
        CreateDate = _createDate;
        ModifiedDate = _modifiedDate;
    }

    protected BaseModel()
    { }
}
