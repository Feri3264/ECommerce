using System.ComponentModel.DataAnnotations;
using ECommerce.Application.Common;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Domain.User;
using ECommerce.Domain.Wishlist;
using ErrorOr;
using MediatR;

namespace ECommerce.Application.User.Commands.RegisterUser;

public class RegisterUserCommandHandler
    (IUserRepository _userRepository,
        IWishlistRepository _wishlistRepository) : IRequestHandler<RegisterUserCommand , ErrorOr<UserModel>>
{
    public async Task<ErrorOr<UserModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.IsUsernameExistsAsync(request.username))
        {
            return UserError.UsernameAlreadyExists;
        }

        var verifyEmail = new EmailAddressAttribute();
        if (!verifyEmail.IsValid(request.email))
        {
            return UserError.EmailNotValid;
        }
        
        if (await _userRepository.IsEmailExistsAsync(request.email))
        {
            return UserError.EmailAlreadyExists;
        }

        var verifyPassword = PasswordService.VlaidatePassword(request.password);
        if (verifyPassword.IsError)
        {
            return verifyPassword.Errors;
        }
        
        
        var newUser = new UserModel(
            _name: request.name,
            _email: request.email,
            _username: request.username,
            _password: request.password,
            _isAdmin: request.isAdmin,
            _isEditor: request.isEditor,
            _isDelete: false,
            _createDate: DateTime.Now, 
            _modifiedDate: DateTime.Now);

        var newWishlist = new WishlistModel(
            _userId: newUser.Id,
            _createDate: DateTime.Now, 
            _modifiedDate: DateTime.Now);
        newUser.ChangeWishlistId(newWishlist.Id);

        await _wishlistRepository.AddAsync(newWishlist);
        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveChangesAsync();
        return newUser;
    }
}