using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Group;
using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Queries.GetSubgroupsByGroup;

public class GetSubgroupsByGroupQueryHandler
    (ISubgroupRepository _subgroupRepository,
        IGroupRepository _groupRepository) : IRequestHandler<GetSubgroupsByGroupQuery , ErrorOr<IEnumerable<SubgroupModel>>>
{
    public async Task<ErrorOr<IEnumerable<SubgroupModel>>> Handle(GetSubgroupsByGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.groupId);
        if (group is null)
        {
            return GroupError.GroupNotFound;
        }

        if (group.SubgroupIds is null)
        {
            return Error.NotFound();
        }
        
        var subgroups = new List<SubgroupModel>();
        foreach (var item in group.SubgroupIds)
        {
            subgroups.Add(await _subgroupRepository.GetByIdAsync(item));
        }

        switch (request.sort)
        {
            case "createDate" :
                subgroups = subgroups.OrderBy(subgroup => subgroup.CreateDate).ToList();
                break;
            case "name" :
                subgroups = subgroups.OrderBy(subgroup => subgroup.Name).ToList();
                break;
        }

        if (request.descending)
            subgroups.Reverse();

        return subgroups;
    }
}
