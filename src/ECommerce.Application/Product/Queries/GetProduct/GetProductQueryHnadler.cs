using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Product;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Product.Queries.GetProduct;

public class GetProductQueryHnadler
    (IProductRepository _productRepository) : IRequestHandler<GetProductQuery , ErrorOr<ProductModel>>
{
    public async Task<ErrorOr<ProductModel>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.id);
        if (product is null)
        {
            return ProductError.ProductNotFound;
        }

        return product;
    }
}