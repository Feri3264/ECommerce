using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Commands.DeleteSubgroupFromGroup;

public record DeleteSubgroupFromGroupCommand
(Guid groupId , Guid subgroupId) : IRequest<ErrorOr<Success>>;
