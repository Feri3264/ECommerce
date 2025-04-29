using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Commands.LoginUser;

public record LoginUserCommand
    (string emailOrUsername, string password) : IRequest<ErrorOr<UserModel>>;