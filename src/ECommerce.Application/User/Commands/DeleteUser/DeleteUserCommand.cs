using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Commands.DeleteUser;

public record DeleteUserCommand
(Guid id) : IRequest<ErrorOr<Success>>;
