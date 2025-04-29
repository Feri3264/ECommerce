using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Queries.GetUser;

public class GetUserQueryHandler
(IUserRepository _userRepository) : IRequestHandler<GetUserQuery, ErrorOr<UserModel>>
{
    public async Task<ErrorOr<UserModel>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.id);
        if (user is null)
        {
            return UserError.UserNotFound;
        }

        return user;
    }
}
