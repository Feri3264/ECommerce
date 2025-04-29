using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Address;
using ECommerce.Domain.Shopcart;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Command.AddAddressToShopcart;

public class AddAddressToShopcartCommandHandler
    (IAddressRepository _addressRepository,
        IShopcartRepository _shopcartRepository) : IRequestHandler<AddAddressToShopcartCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddAddressToShopcartCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.addressId);
        if (address is null)
        {
            return AddressError.AddressNotFound;
        }
        
        var shopcart = await _shopcartRepository.GetByIdAsync(request.shopcartId);
        if (shopcart is null)
        {
            return ShopcartError.ShopcartNotFound;
        }

        shopcart.AddAddress(request.addressId);

        _shopcartRepository.Update(shopcart);
        await _shopcartRepository.SaveChangesAsync();
        return Result.Success;
    }
}