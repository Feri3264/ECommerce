using ECommerce.Application.Common.Auth;
using ErrorOr;

namespace ECommerce.Infrastructure.Common.Auth;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        return "";
    }

    public ErrorOr<Success> VlaidatePassword(string password)
    {
        if (password.Length <= 8)
        {
            return Error.Validation(code: "password.8.char" , description: "Password must be at least 8 characters");
        }
        
        if (!password.Any(c => IsLetter(c)))
        {
            return Error.Validation(code: "password.contain.character" , description: "Password must contain at least one character");
        }

        if (!password.Any(c => IsDeigit(c)))
        {
            return Error.Validation(code: "password.contain.number" , description: "Password must contain at least one number");
        }
        
        return Result.Success;
    }

    #region tools

    private bool IsLetter(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
    }

    private bool IsDeigit(char c)
    {
        return (c >= '0' && c <= '9');
    }

    #endregion
}