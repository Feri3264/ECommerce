using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Queries.GetUsers;

public record GetUsersQuery
() : IRequest<IEnumerable<UserModel>>;
