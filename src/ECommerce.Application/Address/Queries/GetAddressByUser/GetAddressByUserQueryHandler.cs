using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Address;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Address.Queries.GetAddressByUser;

public class GetAddressByUserQueryHandler
    (IAddressRepository _addressRepository) : IRequestHandler<GetAddressByUserQuery , ErrorOr<AddressModel>>
{
    public async Task<ErrorOr<AddressModel>> Handle(GetAddressByUserQuery request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByUserIdAsync(request.userId, request.addressId);
        if (address is null)
        {
            return AddressError.AddressNotFound;
        }
        
        return address;
    }
}