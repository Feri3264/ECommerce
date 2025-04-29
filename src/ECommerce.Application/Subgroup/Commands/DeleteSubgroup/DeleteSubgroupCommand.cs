using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Commands.DeleteSubgroup;

public record DeleteSubgroupCommand
(Guid id) : IRequest<ErrorOr<Success>>;