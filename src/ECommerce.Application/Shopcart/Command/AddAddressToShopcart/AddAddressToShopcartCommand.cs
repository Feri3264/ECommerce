using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Command.AddAddressToShopcart;

public record AddAddressToShopcartCommand
    (Guid shopcartId, Guid addressId) : IRequest<ErrorOr<Success>>;