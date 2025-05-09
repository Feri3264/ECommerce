using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Queries.GetSubgroupsByGroup;

public record GetSubgroupsByGroupQuery
(Guid groupId , bool descending , string sort = "createDate") : IRequest<ErrorOr<IEnumerable<SubgroupModel>>>;