using ErrorOr;
using MediatR;

namespace ECommerce.Application.Address.Command.DeleteAddress;

public record DeleteAddressCommand
    (Guid userId , Guid addressId) : IRequest<ErrorOr<Success>>;