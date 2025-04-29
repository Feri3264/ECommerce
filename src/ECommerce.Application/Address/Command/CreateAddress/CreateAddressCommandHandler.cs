using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Address;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Address.Command.CreateAddress;

public class CreateAddressCommandHandler
    (IUserRepository _userRepository,
        IAddressRepository _addressRepository) : IRequestHandler<CreateAddressCommand, ErrorOr<AddressModel>>
{
    public async Task<ErrorOr<AddressModel>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId);
        if (user is null)
        { 
            return UserError.UserNotFound;
        }

        var newAddress = new AddressModel
            (_userId: user.Id,
                _country: request.country,
                _city: request.city,
                _street: request.street,
                _alley: request.alley,
                _plate: request.plate,
                _createDate: DateTime.Now,
                _modifiedDate: DateTime.Now);
        await _addressRepository.AddAsync(newAddress);
        
        user.AddAddress(newAddress.Id);
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();

        return newAddress;
    }
}