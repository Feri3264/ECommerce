using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.OrderItem;
using ECommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.OrderItem.Persistance;

internal class OrderItemRepository
    (ECommerceDbContext _dbContext) : IOrderItemRepository
{
    public async Task<OrderItemModel> GetByIdAsync(Guid id)
    {
        return await _dbContext.OrderItems.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<OrderItemModel> GetByShopcartProductAsync(Guid shopcartId, Guid productId)
    {
        return await _dbContext.OrderItems.FirstOrDefaultAsync(o => o.ShopcartId == shopcartId && o.ProductId == productId);
    }

    public async Task AddAsync(OrderItemModel model)
    {
        await _dbContext.OrderItems.AddAsync(model);
    }

    public void Delete(OrderItemModel model)
    {
        _dbContext.OrderItems.Remove(model);
    }

    public void Update(OrderItemModel model)
    {
        _dbContext.OrderItems.Update(model);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}