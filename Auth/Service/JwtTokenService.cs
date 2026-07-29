using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Notes.Manager.Auth.Config;

namespace Notes.Manager.Auth.Service;

public class JwtTokenService(JwtOptions jwtOptions)
{
    public string GenerateToken(string email)
    {
        var now = DateTime.UtcNow;
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtOptions.JwtSecret)
        );
        var token = new JwtSecurityToken
        (
            jwtOptions.Issuer,
            jwtOptions.Audience,
            [new Claim(ClaimTypes.Email, email)],
            expires: now.AddSeconds(jwtOptions.JwtExpiration),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}