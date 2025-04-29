using ECommerce.Domain.Subgroup;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Subgroup.Queries.GetSubgroup;

public record GetSubgroupQuery
(Guid id) : IRequest<ErrorOr<SubgroupModel>>;
