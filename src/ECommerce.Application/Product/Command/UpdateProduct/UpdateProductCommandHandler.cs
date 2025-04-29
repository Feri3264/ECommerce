using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Product;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Product.Command.UpdateProduct;

public class UpdateProductCommandHandler
    (IProductRepository _productRepository) : IRequestHandler<UpdateProductCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.id);
        if (product is null)
        {
            return ProductError.ProductNotFound;
        }
        
        product.ChangeName(request.name);
        product.ChangePrice(request.price);
        product.ChangeShortDesc(request.shortDesc);
        product.ChangeFullDesc(request.fullDesc);
        product.ChangeAllowComments(request.allowUserComments);
        product.ChangeModifiedDate(DateTime.Now);
        
        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();
        return Result.Success;
    }
}