using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Address;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Address.Command.UpdateAddress;

public class UpdateAddressCommandHandler
    (IAddressRepository _addressRepository) : IRequestHandler<UpdateAddressCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.id);
        if (address is null)
        {
            return AddressError.AddressNotFound;
        }
        
        address.ChangeCountry(request.country);
        address.ChangeCity(request.city);
        address.ChangeStreet(request.street);
        address.ChangeAlley(request.alley);
        address.ChangePlate(request.plate);
        address.ChangeModifiedDate(DateTime.Now);
        
        _addressRepository.Update(address);
        await _addressRepository.SaveChangesAsync();
        return Result.Success;
    }
}