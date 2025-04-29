using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Product;
using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Commands.DeleteProductFromSubgroup;

public class DeleteProductFromSubgroupCommandHandler
(ISubgroupRepository _subgroupRepository,
IProductRepository _productRepository) : IRequestHandler<DeleteProductFromSubgroupCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteProductFromSubgroupCommand request, CancellationToken cancellationToken)
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

        subgroup.RemoveProduct(request.productId);
        product.UnsetSubgroup();

        _subgroupRepository.Update(subgroup);
        _productRepository.Update(product);
        await _subgroupRepository.SaveChangesAsync();
        return Result.Success;
    }
}
