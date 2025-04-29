using ECommerce.Domain.Product;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Queries.GetProductsBySubgroup;

public record GetProductsBySubgroupQuery
(Guid subgroupId) : IRequest<ErrorOr<IEnumerable<ProductModel>>>;
