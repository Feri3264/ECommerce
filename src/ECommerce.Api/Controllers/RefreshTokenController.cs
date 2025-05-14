using ECommerce.Application.Common.Auth;
using ECommerce.Application.RefreshToken.Command.GenerateRefreshToken;
using ECommerce.Application.RefreshToken.Query.GetRefreshToken;
using ECommerce.Application.RefreshToken.Query.GetUserByRefreshToken;
using ECommerce.Contracts.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
public class RefreshTokenController
    (IMediator _mediator,
        IJwtTokenService jwtTokenService) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var userCommand = new GetUserByRefreshTokenQuery(request.refreshToken);
        var user = await _mediator.Send(userCommand);
        if (user.IsError)
        {
            return NotFound("Invalid refresh token");
        }
        
        var tokenModelCommand = new GetRefreshTokenQuery(request.refreshToken);
        var tokenModel = await _mediator.Send(tokenModelCommand);
        if (tokenModel.Value.ExpireTime < DateTime.UtcNow)
        {
            return ValidationProblem("Refresh token expired");
        }
        
        var refreshTokenCommand = new GenerateRefreshTokenCommand(user.Value);
        var refreshToken = await _mediator.Send(refreshTokenCommand);

        var jwtToken = jwtTokenService.GenerateJwtToken(user.Value.Id ,
            user.Value.Email ,
            user.Value.Password ,
            user.Value.IsAdmin ,
            user.Value.IsEditor);

        var response = new RefreshTokenResponse(jwtToken, refreshToken);
        
        return Ok(response);
    }
}