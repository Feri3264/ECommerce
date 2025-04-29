using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Commands.DeleteProductFromSubgroup;

public record DeleteProductFromSubgroupCommand
(Guid subgroupId , Guid productId) : IRequest<ErrorOr<Success>>;
