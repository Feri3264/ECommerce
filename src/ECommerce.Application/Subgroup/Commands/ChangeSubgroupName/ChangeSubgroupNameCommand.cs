using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Commands.ChangeSubgroupName;

public record ChangeSubgroupNameCommand
    (Guid id , string newName) : IRequest<ErrorOr<Success>>;