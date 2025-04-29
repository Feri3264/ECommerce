using System;
using ECommerce.Domain.Subgroup;

namespace ECommerce.Application.Common.Interfaces.Repositories;

public interface ISubgroupRepository
{
    Task<SubgroupModel> GetByIdAsync(Guid id);
    Task AddAsync(SubgroupModel model);
    void Update(SubgroupModel model);
    Task SaveChangesAsync();
}
