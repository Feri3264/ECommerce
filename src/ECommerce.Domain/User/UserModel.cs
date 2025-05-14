using System;
using ECommerce.Domain.Address;
using ECommerce.Domain.Common;
using ECommerce.Domain.Shopcart;
using ErrorOr;

namespace ECommerce.Domain.User;

public class UserModel : BaseModel
{
    //props    
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public bool IsAdmin { get; private set; }
    public bool IsEditor { get; private set; }
    public bool IsDelete { get; private set; }
    public string? RefreshToken { get; private set; }

    //navigations
    public readonly List<Guid>? AddressIds = new();
    public Guid? ShopcartId { get; private set; }
    public Guid WishlistId { get; private set; }


    //ctor
    public UserModel(
        string _name,
        string _email,
        string _username,
        string _password,
        bool _isAdmin,
        bool _isEditor,
        bool _isDelete,  
        DateTime _createDate,
        DateTime _modifiedDate,
        Guid? id = null) : base(_createDate , _modifiedDate)
    {        
        Id = id ?? Guid.NewGuid();
        Name = _name;
        Email = _email;
        Username = _username;
        Password =_password;
        IsAdmin = _isAdmin;
        IsEditor = _isEditor;
        IsDelete = _isDelete;
    }
    

    //methods
    public void SetRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
    
    public ErrorOr<Success> AddAddress(Guid id)
    {
        if (AddressIds.Contains(id))
        {
            return UserError.AddressAlreadyExists;
        }

        AddressIds.Add(id);
        return Result.Success;
    }

    public ErrorOr<Success> RemoveAddress(Guid id)
    {
        if (AddressIds is null || !AddressIds.Contains(id))
        {
            return UserError.AddressNotFound;
        }
        
        AddressIds.Remove(id);
        return Result.Success;
    }
    
    public ErrorOr<Success> AddShopcart(Guid id)
    {
        if (ShopcartId == id)
        {
            return UserError.ShopcartAlreadyExists;
        }

        ShopcartId = id;
        return Result.Success;
    }
        
    public ErrorOr<Success> RemoveShopcart()
    {
        if (ShopcartId is null)
        {
            return UserError.ShopcartNotFound;
        }
        
        ShopcartId = null;
        return Result.Success;
    }
    

    public Success ChangeWishlistId(Guid? id)
    {
        WishlistId = id ?? WishlistId;
        return Result.Success;
    }
    
    public void DeleteUser()
    {
        IsDelete = !IsDelete;
    }
    
    private UserModel()
    { }
    
    #region Setters

    public ErrorOr<Success> ChangeName(string? name)
    {
        if (name is null)
        {
            return Error.Validation();
        }
        
        Name = name ?? Name;
        return Result.Success;
    }

    public ErrorOr<Success> ChangeEmail(string? email)
    {
        if (email is null)
        {
            return Error.Validation();
        }
        
        Email = email ?? Email;
        return Result.Success;
    }
    
    public ErrorOr<Success> ChangePassword(string? password)
    {
        if (password is null)
        {
            return Error.Validation();
        }
        
        Password = password ?? Password;
        return Result.Success;
    }
    
    public ErrorOr<Success> ChangeUsername(string? username)
    {
        if (username is null)
        {
            return Error.Validation();
        }
        
        Username = username ?? Username;
        return Result.Success;
    }
    
    public ErrorOr<Success> ChangeAdmin(bool? admin)
    {
        if (admin is null)
        {
            return Error.Validation();
        }
        
        IsAdmin = admin ?? IsAdmin;
        return Result.Success;
    }
    
    public ErrorOr<Success> ChangeEditor(bool? editor)
    {
        if (editor is null)
        {
            return Error.Validation();
        }
        
        IsEditor = editor ?? IsEditor;
        return Result.Success;
    }

    public void ChangeModifiedDate(DateTime? modifiedDate)
    {
        ModifiedDate = modifiedDate ?? ModifiedDate;
    }

    #endregion
}
