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

        switch (request.sort)
        {
            case "createDate":
                products = products.OrderBy(p => p.CreateDate).ToList();
                break;
            case "price" :
                products = products.OrderBy(p => p.Price).ToList();
                break;
            case "name":
                products = products.OrderBy(p => p.Name).ToList();
                break;
            default:
                products = products.OrderBy(p => p.CreateDate).ToList();
                break;
        }

        if (request.descending)
            products.Reverse();

        return products;
    }
}
