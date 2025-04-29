using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Group;
using ECommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Group.Persistance;

internal class GroupRepository
    (ECommerceDbContext _dbContext) : IGroupRepository
{
    public async Task<List<GroupModel>> GetGroupsAync()
    {
        return await _dbContext.Groups.ToListAsync();
    }

    public async Task<GroupModel> GetByIdAsync(Guid id)
    {
        return await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task AddAsync(GroupModel model)
    {
        await _dbContext.Groups.AddAsync(model);
    }
    
    public void Update(GroupModel model)
    {
        _dbContext.Groups.Update(model);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}