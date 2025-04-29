using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Contracts.JwtAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TokenController
    (IConfiguration configuration) : ControllerBase
{
    
    private string TokenKey = configuration["Jwt:Key"];
    private static TimeSpan TokenExpiry = TimeSpan.FromDays(1);
    
    [HttpPost]
    public IActionResult GenerateToken(TokenGenerationRequest request)
    {
        var key = Encoding.UTF8.GetBytes(TokenKey);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var calims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, request.userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, request.email),
            new Claim("userPass", request.password),
        };

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(calims),
            Expires = DateTime.UtcNow.Add(TokenExpiry),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };
        
        var tokenHandler = new JsonWebTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(token);
    }
}