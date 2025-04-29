using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Group;
using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Commands.AddSubgroup;

public class AddSubgroupToGroupCommandHandler
(IGroupRepository _groupRepository,
ISubgroupRepository _subgroupRepository) : IRequestHandler<AddSubgroupToGroupCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddSubgroupToGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.groupId);
        if (group is null)
        {
            return GroupError.GroupNotFound;
        }

        var subgroup = await _subgroupRepository.GetByIdAsync(request.subgroupId);
        if (subgroup is null)
        {
            return SubgroupError.SubgroupNotFound;
        }

        var addSubgroupResult = group.AddSubgroup(request.subgroupId);
        if(addSubgroupResult.IsError)
        {
            return addSubgroupResult.Errors;
        }

        var setGroupResult = subgroup.ChangeGroup(request.groupId);
        if(setGroupResult.IsError)
        {
            return setGroupResult.Errors;
        }

        _groupRepository.Update(group);
        _subgroupRepository.Update(subgroup);
        await _groupRepository.SaveChangesAsync();
        return Result.Success;
    }
}
