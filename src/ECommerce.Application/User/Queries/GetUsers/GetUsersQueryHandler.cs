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
        var users = await _userRepository.GetUsersAsync();
        
        switch (request.sort)
        {
            case "createDate":
                users = users.OrderBy(user => user.CreateDate).ToList();
                break;
            case "name":
                users = users.OrderBy(user => user.Name).ToList();
                break;
            case "email":
                users = users.OrderBy(user => user.Email).ToList();
                break;
            case "username":
                users = users.OrderBy(user => user.Username).ToList();
                break;
        }

        if (request.descending)
            users.Reverse();
        
        return users;
    }
}
