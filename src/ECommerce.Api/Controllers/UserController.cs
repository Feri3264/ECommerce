using System.Security.AccessControl;
using ECommerce.Application.Common.Auth;
using ECommerce.Application.User.Commands.ChangePassword;
using ECommerce.Application.User.Commands.DeleteUser;
using ECommerce.Application.User.Commands.LoginUser;
using ECommerce.Application.User.Commands.RegisterUser;
using ECommerce.Application.User.Commands.UpdateUser;
using ECommerce.Application.User.Queries.GetUser;
using ECommerce.Application.User.Queries.GetUsers;
using ECommerce.Contracts.User;
using ECommerce.Domain.User;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace ECommerce.Api.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
public class UserController
    (IMediator _mediator,
        IJwtGenerator _jwtGenerator) : ApiController
{
    
    #region LoginUser
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserRequestDTO request)
    {
        var command = new LoginUserCommand(request.emailOrUsername , request.password);
        var response = await _mediator.Send(command);
        
        return response.Match(
            user => Ok(new UserResponse(
                userId: user.Id,
                name: user.Name,
                email: user.Email,
                username: user.Username,
                password: user.Password,
                isAdmin: user.IsAdmin,
                isEditor: user.IsEditor,
                isDelete: user.IsDelete,
                modifiedDate: user.ModifiedDate,
                createDate: user.CreateDate)),
            Problem);
    }
    
    #endregion

    #region RegisterUser
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDTO request)
    {
        var command = new RegisterUserCommand
            (name : request.name,
                email : request.email,
                username : request.username,
                password : request.password,
                isAdmin : request.isAdmin,
                isEditor : request.isEditor);
        var response = await _mediator.Send(command);

        var token = _jwtGenerator.GenerateJwtToken(response.Value.Id ,
            response.Value.Email ,
            response.Value.Password ,
            response.Value.IsAdmin ,
            response.Value.IsEditor);
        
        return response.Match(
            user => CreatedAtAction(nameof(LoginUser),
                new LoginUserRequestDTO(emailOrUsername: user.Email , password:user.Password) ,
                new RegisterUserResponse(
                userId: user.Id,
                name: user.Name,
                email: user.Email,
                username: user.Username,
                password: user.Password,
                isAdmin: user.IsAdmin,
                isEditor: user.IsEditor,
                JwtToken: token,
                modifiedDate: user.ModifiedDate,
                createDate: user.CreateDate)),
            Problem);
    }

    #endregion
    
    #region ChangePassword

    [HttpPost("{userId:guid}")]
    public async Task<IActionResult> ChangePassword([FromRoute] Guid userId,
        [FromBody] ChangePasswordRequestDTO request)
    {
        var command = new ChangePasswordCommand(
            userId: userId,
            newPassword: request.newPassword,
            oldPassword: request.oldPassword);
        
        var response = await _mediator.Send(command);

        return response.Match(_ => Ok(), Problem);
    }
    
    #endregion
    
    #region GetUser

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var command = new GetUserQuery(id);
        var getUserResult = await _mediator.Send(command);
        
        return getUserResult.Match<IActionResult>(
            user => Ok(new UserResponse
            (userId : user.Id,
                name: user.Name,
                email: user.Email,
                username: user.Username,
                password: user.Password,
                isAdmin: user.IsAdmin,
                isEditor: user.IsEditor,
                isDelete: user.IsDelete,
                createDate: user.CreateDate,
                modifiedDate: user.ModifiedDate)),
            Problem);
    }
    
    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetUsers([FromQuery] string? sort , [FromQuery] bool descending = false)
    {
        var command = new GetUsersQuery( descending , sort);
        var getUsersResult = await _mediator.Send(command);
        
        List<UserResponse> users = new List<UserResponse>();
        foreach (var user in getUsersResult)
        {
            users.Add(new UserResponse
            (userId: user.Id,
                name: user.Name,
                email: user.Email,
                username: user.Username,
                password: user.Password,
                isAdmin: user.IsAdmin,
                isEditor: user.IsEditor,
                isDelete: user.IsDelete,
                createDate: user.CreateDate,
                modifiedDate: user.ModifiedDate));
        }

        return Ok(users);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserForEdit([FromRoute] Guid id)
    {
        var command = new GetUserQuery(id);
        var response = await _mediator.Send(command);

        return response.Match(
            user => Ok(new GetUserForEditResponse(
                name: user.Name,
                email: user.Email,
                username: user.Username)),
            Problem);
    }

    #endregion

    #region UpdateUser
    
    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> PutUser([FromRoute] Guid userId ,[FromBody] UpdateUserRequestDTO request)
    {
        var command = new UpdateUserCommand
            (id: userId,
                name: request.name,
                email: request.email,
                username: request.username,
                password: request.password,
                isAdmin: request.isAdmin,
                isEditor: request.isEditor);
        
        var updateUserResult = await _mediator.Send(command);
        
        return updateUserResult.Match(
            _ => NoContent(),
            Problem);
    }
    //Json Patch Format
    /*
        [
             {
               "op": "replace",
               "path": "/username",
               "value": "farid1010"
             }
        ]
     */
    [HttpPatch("{userId:guid}")]
    public async Task<IActionResult> PatchUser([FromRoute] Guid userId , [FromBody] JsonPatchDocument<PatchUpdateUserRequestDTO> patchDocument)
    {
        var exitingUser = await _mediator.Send(new GetUserQuery(userId));
        if (exitingUser.IsError)
        {
            return NotFound();
        }

        var userToPatch = new PatchUpdateUserRequestDTO
            (name : exitingUser.Value.Name,
                email : exitingUser.Value.Email,
                username : exitingUser.Value.Username,
                password : exitingUser.Value.Password,
                isAdmin : exitingUser.Value.IsAdmin,
                isEditor : exitingUser.Value.IsEditor);
        
        patchDocument.ApplyTo(userToPatch , ModelState);

        var command = new UpdateUserCommand(
            id: userId,
            name : userToPatch.name,
            email : userToPatch.email,
            username : userToPatch.username,
            password : userToPatch.password,
            isAdmin : userToPatch.isAdmin,
            isEditor : userToPatch.isEditor);
        
        var response = await _mediator.Send(command);
        
        return response.Match(
            _ => NoContent(),
            Problem);
    }
    
    #endregion

    #region DeleteUser

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var command = new DeleteUserCommand(id);
        var deleteUserResult = await _mediator.Send(command);

        return deleteUserResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }

    #endregion
    
}