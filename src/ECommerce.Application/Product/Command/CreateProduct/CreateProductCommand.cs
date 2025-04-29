using ECommerce.Domain.Product;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Product.Command.CreateProduct;

public record CreateProductCommand
    (string name,
        int price,
        string shortDesc,
        string fullDesc,
        bool allowUserComments) : IRequest<ErrorOr<ProductModel>>;