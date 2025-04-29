using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Group;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Commands.ChangeGroupName;

public class ChangeGroupNameCommandHandler
    (IGroupRepository _groupRepository) : IRequestHandler<ChangeGroupNameCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(ChangeGroupNameCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.id);
        if (group is null)
        {
            return GroupError.GroupNotFound;
        }
        
        group.ChangeName(request.newName);
        
        _groupRepository.Update(group);
        await _groupRepository.SaveChangesAsync();
        return Result.Success;
    }
}