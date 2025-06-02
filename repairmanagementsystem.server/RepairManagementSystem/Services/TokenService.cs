using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;
using RepairManagementSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

public class TokenService : ITokenService
{
    private readonly RSA _privateKey;
    private readonly RSA _publicKey;
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly ILogger<TokenService> _logger;

    public TokenService(IUserTokenRepository userTokenRepository, ILogger<TokenService> logger)
    {
        _privateKey = RSA.Create();
        _publicKey = RSA.Create();

        string privateKeyBase64 = Env.GetString("JWT_PRIVATE_KEY");
        string publicKeyBase64 = Env.GetString("JWT_PUBLIC_KEY");

        byte[] privateKeyBytes = Convert.FromBase64String(privateKeyBase64);
        _privateKey.ImportPkcs8PrivateKey(privateKeyBytes, out _);

        byte[] publicKeyBytes = Convert.FromBase64String(publicKeyBase64);
        _publicKey.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

        _userTokenRepository = userTokenRepository;
        _logger = logger;
    }

    public string GenerateToken(UserDTO user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.FirstName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role),
            new("userId", user.UserId.ToString()),
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

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        var refreshToken = Convert.ToBase64String(randomNumber);
        return refreshToken;
    }

    public string HashToken(string token)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    public Task<bool> RefreshTokenExists(string hashedRefreshToken)
    {
        return _userTokenRepository.RefreshTokenExists(hashedRefreshToken);
    }

    public Task<int> GetUserIdByRefreshToken(string hashedRefreshToken)
    {
        return _userTokenRepository.GetUserIdByRefreshToken(hashedRefreshToken);
    }

    public int? GetUserIdFromToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type is "userId" or ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                _logger.LogWarning("UserId claim not found in token.");
                return null;
            }
            if (int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            _logger.LogWarning("UserId claim in token is not a valid integer.");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to extract userId from token.");
            return null;
        }
    }
}
