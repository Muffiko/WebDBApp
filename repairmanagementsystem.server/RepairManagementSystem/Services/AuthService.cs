using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly ICryptoService _cryptoService;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthService> _logger;

    public AuthService(ITokenService tokenService, IUserService userService, IUserTokenRepository userTokenRepository, ICryptoService cryptoService, IMapper mapper, ILogger<AuthService> logger)
    {
        _tokenService = tokenService;
        _userService = userService;
        _userTokenRepository = userTokenRepository;
        _cryptoService = cryptoService;
        _mapper = mapper;
        _logger = logger;

    }

    public async Task<AuthResult> AuthenticateAsync(LoginRequest request)
    {
        var user = await _userService.GetUserByEmailAsync(request.Email!);
        if (user == null || !_cryptoService.VerifyPassword(request.Password!, user.PasswordHash, user.PasswordSalt!))
        {
            _logger.LogWarning("Failed login attempt for email: {Email}", request.Email);
            return new AuthResult
            {
                Success = false,
                Response = null,
                ErrorMessage = "Wrong email or password"
            };
        }
        var userDTO = _mapper.Map<UserDTO>(user);

        var existingUserToken = await _userTokenRepository.GetUserTokenByUserIdAsync(userDTO.UserId);
        if (existingUserToken != null)
        {
            await _userTokenRepository.DeleteUserTokenAsync(existingUserToken.UserTokenId);
            _logger.LogInformation("Deleted existing refresh token for userId: {UserId}", userDTO.UserId);
        }

        var token = _tokenService.GenerateToken(userDTO);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var hashedRefreshToken = _tokenService.HashToken(refreshToken);

        var userToken = new UserToken
        {
            UserId = userDTO.UserId,
            RefreshToken = hashedRefreshToken,
            CreatedAt = DateTime.UtcNow,
            ValidUntil = DateTime.UtcNow.AddDays(30),
        };
        await _userTokenRepository.AddUserTokenAsync(userToken);
        _logger.LogInformation("User {Email} authenticated successfully. Token and refresh token created.", userDTO.Email);

        return new AuthResult
        {
            Success = true,
            Response = new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                Email = userDTO.Email,
                Role = userDTO.Role,
                FirstName = userDTO.FirstName,
            },
            ErrorMessage = null
        };
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        var (Hash, Salt) = _cryptoService.HashPassword(request.Password!);
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email!,
            PasswordHash = Hash,
            PasswordSalt = Salt,
            Number = request.PhoneNumber,
            Address = request.Address,
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };

        var userRegisteredResponse = await _userService.RegisterUserAsync(user);
        if (userRegisteredResponse == false)
        {
            _logger.LogWarning("Registration failed for email: {Email}", request.Email);
            return new AuthResult
            {
                Success = false,
                Response = null,
                ErrorMessage = "Registration failed. Please check your input."
            };
        }

        var userDTO = _mapper.Map<UserDTO>(user);

        var token = _tokenService.GenerateToken(userDTO);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var hashedRefreshToken = _tokenService.HashToken(refreshToken);

        var userToken = new UserToken
        {
            UserId = user.UserId,
            RefreshToken = hashedRefreshToken,
            CreatedAt = DateTime.UtcNow,
            ValidUntil = DateTime.UtcNow.AddDays(30),
        };
        await _userTokenRepository.AddUserTokenAsync(userToken);
        _logger.LogInformation("User {Email} registered successfully. Token and refresh token created.", user.Email);

        return new AuthResult
        {
            Success = true,
            Response = new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                Email = user.Email,
                Role = user.Role,
                FirstName = user.FirstName
            },
            ErrorMessage = null
        };
    }
    public async Task<RefreshTokenResponse?> RefreshTokenAsync(string refreshToken)
    {

        var hashedRefreshToken = _tokenService.HashToken(refreshToken);
        if (!await _tokenService.RefreshTokenExists(hashedRefreshToken))
        {
            _logger.LogWarning("Refresh token does not exist or is invalid.");
            return null;
        }
        var userId = await _tokenService.GetUserIdByRefreshToken(hashedRefreshToken!);
        if (userId == -1)
        {
            _logger.LogWarning("Refresh token does not map to a valid user.");
            return null;
        }
        var userDTO = await _userService.GetUserByIdAsync(userId);

        var existingUserToken = await _userTokenRepository.GetUserTokenByUserIdAsync(userDTO!.UserId);
        if (existingUserToken != null)
        {
            await _userTokenRepository.DeleteUserTokenAsync(existingUserToken.UserTokenId);
            _logger.LogInformation("Deleted old refresh token for userId: {UserId}", userDTO.UserId);
        }

        var token = _tokenService.GenerateToken(userDTO);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        var newHashedRefreshToken = _tokenService.HashToken(newRefreshToken);

        var newUserToken = new UserToken
        {
            UserId = userDTO.UserId,
            RefreshToken = newHashedRefreshToken,
            CreatedAt = DateTime.UtcNow,
            ValidUntil = DateTime.UtcNow.AddDays(30),
        };
        await _userTokenRepository.AddUserTokenAsync(newUserToken);
        _logger.LogInformation("Refresh token used for userId: {UserId}. New token and refresh token issued.", userDTO.UserId);

        return new RefreshTokenResponse
        {
            Token = token,
            RefreshToken = newRefreshToken
        };
    }
    public int? GetUserIdFromToken(string token)
    {
        return _tokenService.GetUserIdFromToken(token);
    }

    public async Task DeleteRefreshTokenAsync(string refreshToken)
    {
        var hashedRefreshToken = _tokenService.HashToken(refreshToken);
        var userToken = await _userTokenRepository.GetUserTokenByRefreshToken(hashedRefreshToken);
        if (userToken != null)
        {
            await _userTokenRepository.DeleteUserTokenAsync(userToken.UserTokenId);
            _logger.LogInformation("Refresh token deleted from database for logout.");
        }
        else
        {
            _logger.LogWarning("Attempted to delete a non-existing refresh token during logout.");
        }
    }
}
