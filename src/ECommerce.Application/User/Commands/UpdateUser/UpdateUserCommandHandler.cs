using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Commands.UpdateUser;

public class UpdateUserCommandHandler
    (IUserRepository _userRepository) : IRequestHandler<UpdateUserCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.id);
        if (user is null)
        {
            return UserError.UserNotFound;
        }
        
        user.ChangeName(request.name);
        user.ChangeEmail(request.email);
        user.ChangeUsername(request.username);
        user.ChangePassword(request.password);
        user.ChangeAdmin(request.isAdmin ?? user.IsAdmin);
        user.ChangeEditor(request.isEditor ?? user.IsEditor);
        user.ChangeModifiedDate(DateTime.Now);

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
        return Result.Success;
    }
}
