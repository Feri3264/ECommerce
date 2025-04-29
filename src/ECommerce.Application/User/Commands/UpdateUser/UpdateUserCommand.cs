using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Commands.UpdateUser;

public record UpdateUserCommand
(Guid id,
    string? name,
    string? email,
    string? username,
    string? password,
    bool? isAdmin,
    bool? isEditor) : IRequest<ErrorOr<Success>>;
