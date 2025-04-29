using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Commands.DeleteGroup;

public record DeleteGroupCommand
(Guid id) : IRequest<ErrorOr<Success>>;
