using System;
using ECommerce.Domain.Product;
using ECommerce.Domain.Subgroup;
namespace ECommerce.Application.Common.Interfaces.Repositories;

public interface IProductRepository
{
    Task<ProductModel> GetByIdAsync(Guid id);
    Task<bool> AnyAsync(Guid id);
    Task AddAsync(ProductModel model);
    void Update(ProductModel model);
    Task SaveChangesAsync();
}