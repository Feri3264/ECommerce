using ECommerce.Domain.ShopcartProductMapper;

namespace ECommerce.Application.Common.Interfaces.Repositories;

public interface IShopcartProductMapperRepository
{
    Task<ShopcartProductMapperModel> GetByIdAsync(Guid shopcartId , Guid productId);
    Task AddAsync(ShopcartProductMapperModel model);
    void Delete(ShopcartProductMapperModel model);
}