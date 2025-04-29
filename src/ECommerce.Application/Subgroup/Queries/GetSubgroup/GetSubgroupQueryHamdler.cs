using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Queries.GetSubgroup;

public class GetSubgroupQueryHamdler
(ISubgroupRepository _subgroupRepository) : IRequestHandler<GetSubgroupQuery, ErrorOr<SubgroupModel>>
{
    public async Task<ErrorOr<SubgroupModel>> Handle(GetSubgroupQuery request, CancellationToken cancellationToken)
    {
        var subgroup = await _subgroupRepository.GetByIdAsync(request.id);
        if(subgroup is null)
        {
            return SubgroupError.SubgroupNotFound;
        }

        return subgroup;
    }
}
