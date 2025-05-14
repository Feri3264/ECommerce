using ECommerce.Domain.Common;

namespace ECommerce.Domain.RefreshToken;

public class RefreshTokenModel : BaseModel
{
    public string Token { get; private set; }
    public DateTime ExpireTime { get; private set; }
    
    //navigation
    public Guid UserId { get; set; }
    
    //ctor
    public RefreshTokenModel(string _token, DateTime _expireTime, Guid _userId, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Token = _token;
        ExpireTime = _expireTime;
        UserId = _userId;
    }

    private RefreshTokenModel() { }
}