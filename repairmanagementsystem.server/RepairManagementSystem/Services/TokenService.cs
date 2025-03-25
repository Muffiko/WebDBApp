using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;

public class TokenService : ITokenService
{
    private readonly string _secretKey;

    public TokenService()
    {
        _secretKey = Env.GetString("JWT_SECRET_KEY");
    }

    public string GenerateToken(UserDTO user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var keyBytes = Encoding.UTF8.GetBytes(_secretKey);
        var key = new SymmetricSecurityKey(keyBytes);

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Env.GetString("JWT_ISSUER"),
            audience: Env.GetString("JWT_AUDIENCE"),
            claims: claims,
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
            ValidateIssuer = true,
            ValidIssuer = Env.GetString("JWT_ISSUER"),
            ValidateAudience = true,
            ValidAudience = Env.GetString("JWT_AUDIENCE"),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
