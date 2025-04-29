using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Product;
using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Commands.AddProductToSubgroup;

public class AddProductToSubgroupCommandHandler
(ISubgroupRepository _subgroupRepository,
IProductRepository _productRepository) : IRequestHandler<AddProductToSubgroupCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddProductToSubgroupCommand request, CancellationToken cancellationToken)
    {
        var subgroup = await _subgroupRepository.GetByIdAsync(request.subgroupId);
        if (subgroup is null)
        {
            return SubgroupError.SubgroupNotFound;
        }

        var product = await _productRepository.GetByIdAsync(request.productId);
        if (product is null)
        {
            return ProductError.ProductNotFound;
        }

        subgroup.AddProduct(request.productId);
        product.ChangeSubgroup(request.subgroupId);

        _subgroupRepository.Update(subgroup);
        _productRepository.Update(product);
        await _subgroupRepository.SaveChangesAsync();
        return Result.Success;
    }
}
