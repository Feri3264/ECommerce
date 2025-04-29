using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Commands.AddProductToSubgroup;

public record AddProductToSubgroupCommand
(Guid subgroupId , Guid productId) : IRequest<ErrorOr<Success>>;
