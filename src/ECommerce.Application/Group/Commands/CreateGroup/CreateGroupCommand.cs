using ECommerce.Domain.Group;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.Group.Commands.CreateGroup;

public record CreateGroupCommand
(string name) : IRequest<ErrorOr<GroupModel>>;
