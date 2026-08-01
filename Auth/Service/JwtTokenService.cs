using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Notes.Manager.Auth.Config;
using Notes.Manager.Users.Domain;

namespace Notes.Manager.Auth.Service;

public class JwtTokenService(JwtOptions jwtOptions)
{
    public string GenerateToken(long userId, Role userRole)
    {
        var now = DateTime.UtcNow;
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtOptions.JwtSecret)
        );
        var token = new JwtSecurityToken
        (
            jwtOptions.Issuer,
            jwtOptions.Audience,
            [new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, userRole.GetAuthority())],
            expires: now.AddSeconds(jwtOptions.JwtExpiration),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}