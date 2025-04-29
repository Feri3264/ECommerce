using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Group;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Queries.GetGroups;

public class GetGroupsQueryHandler
(IGroupRepository _groupRepository) : IRequestHandler<GetGroupsQuery, IEnumerable<GroupModel>>
{
    public async Task<IEnumerable<GroupModel>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetGroupsAync();
        return groups;
    }
}