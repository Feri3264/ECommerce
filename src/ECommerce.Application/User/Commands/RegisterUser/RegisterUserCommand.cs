using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Commands.RegisterUser;

public record RegisterUserCommand
    (string name,
        string email,
        string username,
        string password,
        bool isAdmin,
        bool isEditor) : IRequest<ErrorOr<UserModel>>;