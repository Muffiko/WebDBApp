using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;

public class TokenService : ITokenService
{
    private readonly RSA _privateKey;
    private readonly RSA _publicKey;

    public TokenService()
    {
        _privateKey = RSA.Create();
        _publicKey = RSA.Create();

        string privateKeyBase64 = Env.GetString("JWT_PRIVATE_KEY");
        string publicKeyBase64 = Env.GetString("JWT_PUBLIC_KEY");

        byte[] privateKeyBytes = Convert.FromBase64String(privateKeyBase64);
        _privateKey.ImportPkcs8PrivateKey(privateKeyBytes, out _);

        byte[] publicKeyBytes = Convert.FromBase64String(publicKeyBase64);
        _publicKey.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
    }

    public string GenerateToken(UserDTO user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var credentials = new SigningCredentials(
            new RsaSecurityKey(_privateKey),
            SecurityAlgorithms.RsaSha256
        );

        var token = new JwtSecurityToken(
            issuer: Env.GetString("JWT_ISSUER"),
            audience: Env.GetString("JWT_AUDIENCE"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
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
            IssuerSigningKey = new RsaSecurityKey(_publicKey),
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
