using ECommerce.Domain.Address;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Address.Queries.GetAddressesByUser;

public record GetAddressesByUserQuery
    (Guid userId) : IRequest<ErrorOr<IEnumerable<AddressModel>>>;