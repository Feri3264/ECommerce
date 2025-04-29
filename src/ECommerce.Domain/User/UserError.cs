using System;
using ErrorOr;

namespace ECommerce.Domain.User;

public class UserError
{
    public static Error ShopcartAlreadyExists = Error.Conflict
    (code : "shopcart.already.exists", description : "Shopcart With This Id Already Exists");
    
    public static Error ShopcartNotFound = Error.NotFound
        (code :"shopcart.not.found", description : "Shopcart With This Id Not Found");
    
    public static Error AddressAlreadyExists = Error.Conflict
    (code :"address.already.exists", description : "Address With This Id Already Exists");
    
    public static Error AddressNotFound = Error.NotFound
        (code :"address.not.found", description : "Address With This Id Not Found");

    public static Error UserNotFound = Error.NotFound
    (code :"user.not.found", description : "User With This Id Not Found");
    
    public static Error IncorrectPassword= Error.NotFound
        (code :"incorrect.old.password", description : "Your Password is incorrect");
    
    public static Error LoginValidationFailed= Error.Validation
        (code :"email.username.or.password.incorrect", description : "Your Email/Username Or Password is incorrect");
    
    public static Error UsernameAlreadyExists= Error.Validation
        (code :"username.already.exists", description : "This Username Is Already Taken");
    
    public static Error EmailAlreadyExists= Error.Validation
        (code :"email.already.exists", description : "This Email Is Already Taken");
    
    public static Error EmailNotValid= Error.Validation
        (code :"email.not.valid", description : "This Email Is Not Valid");
}
