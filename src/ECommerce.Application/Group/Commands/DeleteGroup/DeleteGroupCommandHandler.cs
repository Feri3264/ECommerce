using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Group;
using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Commands.DeleteGroup;

public class DeleteGroupCommandHandler
(IGroupRepository _groupRepository,
    ISubgroupRepository _subgroupRepository) : IRequestHandler<DeleteGroupCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.id);
        if (group is null)
        {
            return GroupError.GroupNotFound;
        }
        
        foreach (var item in group.SubgroupIds)
        {
            var subgroup = await _subgroupRepository.GetByIdAsync(item);
            subgroup.DeleteSubgroup();
            _subgroupRepository.Update(subgroup);
        }

        group.DeleteGroup();
        _groupRepository.Update(group);
        await _groupRepository.SaveChangesAsync();
        return Result.Success;
    }
}
