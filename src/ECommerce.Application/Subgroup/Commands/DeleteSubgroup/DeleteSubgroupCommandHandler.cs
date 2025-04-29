using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Commands.DeleteSubgroup;

public class DeleteSubgroupCommandHandler
(ISubgroupRepository _subgroupRepository) : IRequestHandler<DeleteSubgroupCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteSubgroupCommand request, CancellationToken cancellationToken)
    {
        var subgroup = await _subgroupRepository.GetByIdAsync(request.id);
        if (subgroup is null)
        {
            return SubgroupError.SubgroupNotFound;
        }
        
        subgroup.DeleteSubgroup();

        _subgroupRepository.Update(subgroup);
        await _subgroupRepository.SaveChangesAsync();
        return Result.Success;
    }
}
