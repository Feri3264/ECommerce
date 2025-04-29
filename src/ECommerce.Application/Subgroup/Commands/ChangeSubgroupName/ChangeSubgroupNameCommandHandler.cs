using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Commands.ChangeSubgroupName;

public class ChangeSubgroupNameCommandHandler
    (ISubgroupRepository _subgroupRepository) : IRequestHandler<ChangeSubgroupNameCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(ChangeSubgroupNameCommand request, CancellationToken cancellationToken)
    {
        var subgroup = await _subgroupRepository.GetByIdAsync(request.id);
        if (subgroup is null)
        {
            return SubgroupError.SubgroupNotFound;
        }
        
        subgroup.ChangeName(request.newName);
        
        _subgroupRepository.Update(subgroup);
        await _subgroupRepository.SaveChangesAsync();
        return Result.Success;
    }
}