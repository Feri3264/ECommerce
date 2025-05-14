using ECommerce.Domain.User;
using MediatR;

namespace ECommerce.Application.RefreshToken.Command.GenerateRefreshToken;

public record GenerateRefreshTokenCommand
    (UserModel user) : IRequest<string>;