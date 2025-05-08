using ErrorOr;

namespace ECommerce.Application.Common.Auth;

public interface IPasswordService
{
    public ErrorOr<Success> VlaidatePassword(string password);

    public string HashPassword(string password);
}