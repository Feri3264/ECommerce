using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Commands.AddSubgroup;

public record class AddSubgroupToGroupCommand
(Guid groupId , Guid subgroupId) : IRequest<ErrorOr<Success>>;
