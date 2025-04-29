using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Commands.CreateSubgroup;

public record CreateSubgroupCommand
(Guid groupId , string name) : IRequest<ErrorOr<SubgroupModel>>;
