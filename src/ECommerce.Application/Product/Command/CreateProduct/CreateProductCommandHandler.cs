using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Product;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Product.Command.CreateProduct;

public class CreateProductCommandHandler
    (IProductRepository _productRepository) : IRequestHandler<CreateProductCommand , ErrorOr<ProductModel>>
{
    public async Task<ErrorOr<ProductModel>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new ProductModel
            (_name : request.name,
                _price: request.price,
                _shortDesc: request.shortDesc,
                _fullDesc: request.fullDesc,
                _isDelete: false,
                _subgroupId: Guid.Parse("d859e766-fb34-4914-9c8d-27b02c40ffd4"),
                _allowUserComments: true,
                _createDate: DateTime.Now, 
                _modifiedDate: DateTime.Now);

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
        return product;
    }
}