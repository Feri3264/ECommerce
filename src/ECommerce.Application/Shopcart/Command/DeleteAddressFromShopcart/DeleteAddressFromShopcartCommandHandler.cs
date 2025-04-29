using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Shopcart;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Shopcart.Command.DeleteAddressFromShopcart;

public class DeleteAddressFromShopcartCommandHandler
    (IShopcartRepository _shopcartRepository) : IRequestHandler<DeleteAddressFromShopcartCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteAddressFromShopcartCommand request, CancellationToken cancellationToken)
    {
        var shopcart = await _shopcartRepository.GetByIdAsync(request.shopCartId);
        if (shopcart is null)
        {
            return ShopcartError.ShopcartNotFound;
        }
        
        shopcart.RemoveAddress();
        
        _shopcartRepository.Update(shopcart);
        await _shopcartRepository.SaveChangesAsync();
        return Result.Success;
    }
}