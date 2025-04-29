using ECommerce.Domain.Product;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Product.Queries.GetProduct;

public record GetProductQuery
    (Guid id) : IRequest<ErrorOr<ProductModel>>;