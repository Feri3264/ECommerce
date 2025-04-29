using ECommerce.Domain.Address;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Address.Queries.GetAddress;

public record GetAddressQuery
    (Guid addressId) : IRequest<ErrorOr<AddressModel>>;