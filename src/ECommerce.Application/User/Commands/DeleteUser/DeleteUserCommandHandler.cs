using System;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.User;
using ECommerce.Domain.Wishlist;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Commands.DeleteUser;

public class DeleteUserCommandHandler
(IUserRepository _userRepository,
    IWishlistRepository _wishlistRepository) : IRequestHandler<DeleteUserCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        
        var user =await _userRepository.GetByIdAsync(request.id);
        var wishlist = await _wishlistRepository.GetByIdAsync(user.WishlistId);
        if (user is null)
        {
            return UserError.UserNotFound;
        }

        user.DeleteUser();
        wishlist.DeleteWishlist();
        
        _userRepository.Update(user);
        _wishlistRepository.Update(wishlist);
        await _userRepository.SaveChangesAsync();
        return Result.Success;
    }
}
