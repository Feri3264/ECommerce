using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Commands.CreateSubgroup;

public class CreateSubgroupCommandHandler
(ISubgroupRepository _subgroupRepository) : IRequestHandler<CreateSubgroupCommand, ErrorOr<SubgroupModel>>
{
    public async Task<ErrorOr<SubgroupModel>> Handle(CreateSubgroupCommand request, CancellationToken cancellationToken)
    {
        var subgroup = new SubgroupModel
        (_groupId : request.groupId,
        _name : request.name,
        _createDate : DateTime.Now,
        _modifiedDate : DateTime.Now);

        await _subgroupRepository.AddAsync(subgroup);
        await _subgroupRepository.SaveChangesAsync();
        return subgroup;
    }
}
