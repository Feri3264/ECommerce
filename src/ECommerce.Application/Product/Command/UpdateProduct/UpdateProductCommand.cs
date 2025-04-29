using ErrorOr;
using MediatR;

namespace ECommerce.Application.Product.Command.UpdateProduct;

public record UpdateProductCommand
    (Guid id,
        string? name,
        int? price,
        string? shortDesc,
        string? fullDesc,
        bool? allowUserComments) : IRequest<ErrorOr<Success>>;