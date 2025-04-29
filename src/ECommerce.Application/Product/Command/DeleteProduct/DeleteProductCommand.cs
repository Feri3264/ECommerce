using ErrorOr;
using MediatR;

namespace ECommerce.Application.Product.Command.DeleteProduct;

public record DeleteProductCommand
    (Guid id) : IRequest<ErrorOr<Success>>;