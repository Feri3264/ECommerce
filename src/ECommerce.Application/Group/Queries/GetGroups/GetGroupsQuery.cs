using ECommerce.Domain.Group;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Queries.GetGroups;

public record GetGroupsQuery
() : IRequest<IEnumerable<GroupModel>>;