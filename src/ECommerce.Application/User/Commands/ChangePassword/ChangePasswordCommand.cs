using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Commands.ChangePassword;

public record ChangePasswordCommand
    (Guid userId, string newPassword, string oldPassword) : IRequest<ErrorOr<Success>>;