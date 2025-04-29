using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.Product;
using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Queries.GetProductsBySubgroup;

public class GetProductsBySubgroupQueryHandler
(ISubgroupRepository _subgrouprepository,
IProductRepository _productRepository) : IRequestHandler<GetProductsBySubgroupQuery, ErrorOr<IEnumerable<ProductModel>>>
{
    public async Task<ErrorOr<IEnumerable<ProductModel>>> Handle(GetProductsBySubgroupQuery request, CancellationToken cancellationToken)
    {
        var subgroup = await _subgrouprepository.GetByIdAsync(request.subgroupId);
        if (subgroup is null)
        {
            return SubgroupError.SubgroupNotFound;
        }

        var products = new List<ProductModel>();
        foreach(var item in subgroup.ProductIds)
        {
            products.Add(await _productRepository.GetByIdAsync(item));
        }

        return products;
    }
}
