using ECommerce.Domain.Product;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Queries.GetProductsBySubgroup;

public record GetProductsBySubgroupQuery
(Guid subgroupId,
    bool descending,
    string? sort = "createDate") : IRequest<ErrorOr<IEnumerable<ProductModel>>>;
