using ECommerce.Domain.Address;

namespace ECommerce.Application.Common.Interfaces.Repositories;

public interface IAddressRepository
{
    Task<AddressModel> GetByIdAsync(Guid id);
    Task<AddressModel> GetByUserIdAsync(Guid userId , Guid addressId);
    Task AddAsync(AddressModel model);
    void Update(AddressModel model);
    void Delete(AddressModel model);
    Task SaveChangesAsync();
}