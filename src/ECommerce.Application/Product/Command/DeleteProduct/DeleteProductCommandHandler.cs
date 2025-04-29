using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Product;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Product.Command.DeleteProduct;

public class DeleteProductCommandHandler
    (IProductRepository _productRepository) : IRequestHandler<DeleteProductCommand , ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.id);
        if (product is null)
        {
            return ProductError.ProductNotFound;
        }
        
        product.DeleteProduct();
        
        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();
        return Result.Success;
    }
}