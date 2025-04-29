using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Group;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Queries.GetGroup;

public class GetGroupQueryHandler
(IGroupRepository _groupRepository) : IRequestHandler<GetGroupQuery, ErrorOr<GroupModel>>
{
    public async Task<ErrorOr<GroupModel>> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.id);
        if (group is null)
        {
            return GroupError.GroupNotFound;
        }

        return group;
    }
}
