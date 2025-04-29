using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Subgroup;
using ECommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Subgroup.Persistance;

internal class SubgroupRepository
    (ECommerceDbContext _dbContext) : ISubgroupRepository
{
    public async Task<SubgroupModel> GetByIdAsync(Guid id)
    {
        return await _dbContext.Subgroups.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddAsync(SubgroupModel model)
    {
        await _dbContext.Subgroups.AddAsync(model);
    }

    public void Update(SubgroupModel model)
    {
        _dbContext.Subgroups.Update(model);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}