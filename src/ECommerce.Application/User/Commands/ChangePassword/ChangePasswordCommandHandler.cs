using ECommerce.Application.Common;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Commands.ChangePassword;

public class ChangePasswordCommandHandler
    (IUserRepository _userRepository) : IRequestHandler<ChangePasswordCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if (request.newPassword == request.oldPassword)
        {
            return Error.Validation(code: "set.new.password" , description: "Please Set A New Password");
        }
        
        var user = await _userRepository.GetByIdAsync(request.userId);
        if (user is null)
        {
            return UserError.UserNotFound;
        }
        
        if (user.Password != request.oldPassword)
        {
            return UserError.IncorrectPassword;
        }

        var verifyPassword = PasswordService.VlaidatePassword(request.newPassword);
        if (verifyPassword.IsError)
        {
            return verifyPassword.Errors;
        }
        
        user.ChangePassword(request.newPassword);
        user.ChangeModifiedDate(DateTime.Now);
        
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
        return Result.Success;
    }
}