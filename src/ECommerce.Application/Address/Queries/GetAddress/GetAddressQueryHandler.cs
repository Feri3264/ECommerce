using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Address;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Address.Queries.GetAddress;

public class GetAddressQueryHandler
    (IAddressRepository _addressRepository) : IRequestHandler<GetAddressQuery, ErrorOr<AddressModel>>
{
    public async Task<ErrorOr<AddressModel>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.addressId);
        if (address is null)
        {
            return AddressError.AddressNotFound;
        }
        
        return address;
    }
}