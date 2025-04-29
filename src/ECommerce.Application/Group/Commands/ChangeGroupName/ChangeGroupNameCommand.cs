using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Commands.ChangeGroupName;

public record ChangeGroupNameCommand
    (Guid id, string newName) : IRequest<ErrorOr<Success>>;