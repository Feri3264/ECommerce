using ECommerce.Domain.Address;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Address.Queries.GetAddressByUser;

public record GetAddressByUserQuery
    (Guid userId , Guid addressId) : IRequest<ErrorOr<AddressModel>>;