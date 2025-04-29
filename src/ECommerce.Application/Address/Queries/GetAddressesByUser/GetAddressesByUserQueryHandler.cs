using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Address;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Address.Queries.GetAddressesByUser;

public class GetAddressesByUserQueryHandler
    (IUserRepository _userRepository,
        IAddressRepository _addressRepository) : IRequestHandler<GetAddressesByUserQuery , ErrorOr<IEnumerable<AddressModel>>>
{
    public async Task<ErrorOr<IEnumerable<AddressModel>>> Handle(GetAddressesByUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId);
        if (user is null)
        {
            return UserError.UserNotFound;
        }

        var addresses = new List<AddressModel>();
        foreach (var item in user.AddressIds)
        {
            addresses.Add(await _addressRepository.GetByIdAsync(item));
        }
        
        return addresses;
    }
}