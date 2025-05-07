using ErrorOr;
using MediatR;

namespace ECommerce.Application.Address.Command.UpdateAddress;

public record UpdateAddressCommand
    (Guid id,
        string? country,
        string? city,
        string? street,
        string? alley,
        string? plate,
        Guid? userId) : IRequest<ErrorOr<Success>>;