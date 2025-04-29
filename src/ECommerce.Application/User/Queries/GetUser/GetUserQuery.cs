using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Queries.GetUser;

public record GetUserQuery
(Guid id) : IRequest<ErrorOr<UserModel>>;
