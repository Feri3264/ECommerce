using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.User;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Queries.GetUsers;

public class GetUsersQueryHandler
(IUserRepository _userRepository) : IRequestHandler<GetUsersQuery,IEnumerable<UserModel>>
{
    public async Task<IEnumerable<UserModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUsersAsync();
    }
}
