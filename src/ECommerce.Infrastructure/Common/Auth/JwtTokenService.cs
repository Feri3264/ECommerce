using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Application.Common.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ECommerce.Infrastructure.Common.Auth;

public class JwtTokenService
    (IConfiguration _configuration) : IJwtTokenService
{
    private string TokenKey = _configuration["Jwt:Key"];
    private static TimeSpan TokenExpiry = TimeSpan.FromHours(1);
    
    public string GenerateJwtToken(Guid userId, string email, string password , bool isAdmin = false, bool isEditor = false)
    {
        var key = Encoding.UTF8.GetBytes(TokenKey);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("userId", userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim("userPass", password)
        };
        
        if (isAdmin)
            claims.Add(new Claim(ClaimTypes.Role , "admin"));
        
        if (isEditor)
            claims.Add(new Claim(ClaimTypes.Role, "editor"));
            

        var token = new JwtSecurityToken(
            claims : claims,
            expires : DateTime.UtcNow.Add(TokenExpiry),
            signingCredentials : credentials,
            issuer : _configuration["Jwt:Issuer"],
            audience : _configuration["Jwt:Audience"]);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}