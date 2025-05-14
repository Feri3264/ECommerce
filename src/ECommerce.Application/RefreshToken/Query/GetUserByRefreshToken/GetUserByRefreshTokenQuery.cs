using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.RefreshToken.Query.GetUserByRefreshToken;

public record GetUserByRefreshTokenQuery
    (string refreshToken) : IRequest<ErrorOr<UserModel>>;