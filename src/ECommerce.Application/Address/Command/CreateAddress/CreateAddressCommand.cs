using ECommerce.Domain.Address;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Address.Command.CreateAddress;

public record CreateAddressCommand
    (Guid userId,
        string country,
        string city,
        string street,
        string alley,
        string plate) : IRequest<ErrorOr<AddressModel>>;