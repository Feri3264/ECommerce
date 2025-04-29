using System;
using ECommerce.Domain.Group;

namespace ECommerce.Application.Common.Interfaces.Repositories;

public interface IGroupRepository
{
    Task<List<GroupModel>> GetGroupsAync();
    Task<GroupModel> GetByIdAsync(Guid id);
    Task AddAsync(GroupModel model);
    void Update(GroupModel model);
    Task SaveChangesAsync();
}
