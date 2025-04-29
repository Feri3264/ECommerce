using ECommerce.Domain.OrderItem;

namespace ECommerce.Application.Common.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task<OrderItemModel> GetByIdAsync(Guid id);
    Task<OrderItemModel> GetByShopcartProductAsync(Guid shopcartId,Guid productId);
    Task AddAsync(OrderItemModel model);
    void Delete(OrderItemModel model);
    void Update(OrderItemModel model);
    Task SaveChangesAsync();
}