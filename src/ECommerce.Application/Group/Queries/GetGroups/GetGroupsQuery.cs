using ECommerce.Domain.Group;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Queries.GetGroups;

public record GetGroupsQuery
(bool descending, string sort = "createDate") : IRequest<IEnumerable<GroupModel>>;