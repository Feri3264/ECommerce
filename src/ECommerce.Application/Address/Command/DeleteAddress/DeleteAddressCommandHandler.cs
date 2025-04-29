using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Address;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Address.Command.DeleteAddress;

public class DeleteAddressCommandHandler
    (IUserRepository _userRepository,
        IAddressRepository _addressRepository) : IRequestHandler<DeleteAddressCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId);
        if (user is null)
        {
            return UserError.UserNotFound;
        }
        
        var address = await _addressRepository.GetByUserIdAsync(request.userId , request.addressId);
        if (address is null)
        {
            return AddressError.AddressNotFound;
        }
        
        user.RemoveAddress(address.Id);
        _userRepository.Update(user);
        _addressRepository.Delete(address);
        await _userRepository.SaveChangesAsync();
        
        return Result.Success;
    }
}