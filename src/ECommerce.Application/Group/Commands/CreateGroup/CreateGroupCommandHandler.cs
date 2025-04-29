using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Group;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Commands.CreateGroup;

public class CreateGroupCommandHandler
(IGroupRepository _groupRepository) : IRequestHandler<CreateGroupCommand, ErrorOr<GroupModel>>
{
    public async Task<ErrorOr<GroupModel>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var newGroup = new GroupModel
        (_name : request.name,
        _createDate : DateTime.Now,
        _modifiedDate : DateTime.Now);

        await _groupRepository.AddAsync(newGroup);
        await _groupRepository.SaveChangesAsync();
        return newGroup;
    }
}
