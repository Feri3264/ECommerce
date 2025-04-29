using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Group;
using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Commands.DeleteSubgroupFromGroup;

public class DeleteSubgroupFromGroupCommandHandler
(IGroupRepository _groupRepository,
ISubgroupRepository _subgroupRepository) : IRequestHandler<DeleteSubgroupFromGroupCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteSubgroupFromGroupCommand request, CancellationToken cancellationToken)
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

        var removeSubgropResult = group.RemoveSubgroup(request.subgroupId);
        if(removeSubgropResult.IsError)
        {
            return removeSubgropResult.Errors;
        }

        var unsetGroupResult = subgroup.UnsetGroup();
        if (unsetGroupResult.IsError)
        {
            return unsetGroupResult.Errors;
        }


        _groupRepository.Update(group);
        _subgroupRepository.Update(subgroup);
        await _groupRepository.SaveChangesAsync();
        return Result.Success;
    }
}
