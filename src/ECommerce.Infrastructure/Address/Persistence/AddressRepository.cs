using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Address;
using ECommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Address.Persistance;

internal class AddressRepository
    (ECommerceDbContext _dbContext) : IAddressRepository
{
    public async Task<AddressModel> GetByIdAsync(Guid id)
    {
        return await _dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<AddressModel> GetByUserIdAsync(Guid userId, Guid addressId)
    {
        return await _dbContext.Addresses.FirstOrDefaultAsync(a => a.UserId == userId && a.Id == addressId);
    }

    public async Task AddAsync(AddressModel model)
    {
        await _dbContext.Addresses.AddAsync(model);
    }

    public void Update(AddressModel model)
    {
        _dbContext.Addresses.Update(model);
    }

    public void Delete(AddressModel model)
    {
        _dbContext.Addresses.Remove(model);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}