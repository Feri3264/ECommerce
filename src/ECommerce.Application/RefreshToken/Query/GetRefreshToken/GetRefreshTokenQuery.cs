using ECommerce.Domain.RefreshToken;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.RefreshToken.Query.GetRefreshToken;

public record GetRefreshTokenQuery
    (string refreshToken) : IRequest<ErrorOr<RefreshTokenModel>>;