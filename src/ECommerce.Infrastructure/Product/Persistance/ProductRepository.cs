using System.Collections.Concurrent;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Product;
using ECommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Product.Persistance;

internal class ProductRepository
    (ECommerceDbContext _dbContext) : IProductRepository
{
    public async Task<ProductModel> GetByIdAsync(Guid id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> AnyAsync(Guid id)
    {
        return await _dbContext.Products.AnyAsync(p => p.Id == id);
    }

    public async Task AddAsync(ProductModel model)
    {
        await _dbContext.Products.AddAsync(model);
    }

    public void Update(ProductModel model)
    {
        _dbContext.Products.Update(model);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}