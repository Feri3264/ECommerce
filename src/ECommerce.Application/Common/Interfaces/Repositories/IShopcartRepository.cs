using ECommerce.Domain.Shopcart;

namespace ECommerce.Application.Common.Interfaces.Repositories;

public interface IShopcartRepository
{
    Task<ShopcartModel> GetByIdAsync(Guid Id);
    Task<ShopcartModel> GetByUserIdAsync(Guid userId);
    Task AddAsync(ShopcartModel model);
    void Delete(ShopcartModel model);
    void Update(ShopcartModel model);
    Task SaveChangesAsync();
}