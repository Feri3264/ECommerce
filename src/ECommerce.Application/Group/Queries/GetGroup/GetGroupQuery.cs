using ECommerce.Domain.Group;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Queries.GetGroup;

public record GetGroupQuery
(Guid id) : IRequest<ErrorOr<GroupModel>>;