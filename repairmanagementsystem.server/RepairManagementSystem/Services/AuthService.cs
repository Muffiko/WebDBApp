using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;
using AutoMapper;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly ICryptoService _cryptoService;
    private readonly IMapper _mapper;

    public AuthService(ITokenService tokenService, IUserService userService, IUserTokenRepository userTokenRepository, ICryptoService cryptoService, IMapper mapper)
    {
        _tokenService = tokenService;
        _userService = userService;
        _userTokenRepository = userTokenRepository;
        _cryptoService = cryptoService;
        _mapper = mapper;

    }

    public async Task<AuthResult> AuthenticateAsync(LoginRequest request)
    {
        var user = await _userService.GetUserByEmailAsync(request.Email!);
        if (user == null || !_cryptoService.VerifyPassword(request.Password!, user.PasswordHash, user.PasswordSalt!))
        {
            return new AuthResult
            {
                Success = false,
                Response = null,
                ErrorMessage = "Wrong email or password"
            };
        }
        var userDTO = _mapper.Map<UserDTO>(user);

        //find if refresh token exists for the user
        var existingUserToken = await _userTokenRepository.GetUserTokenByUserIdAsync(userDTO.UserId);
        if (existingUserToken != null)
        {
            await _userTokenRepository.DeleteUserTokenAsync(existingUserToken.UserTokenId);
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
            Number = request.Number,
            Address = request.Address,
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };

        var userRegisteredResponse = await _userService.RegisterUserAsync(user);
        if (userRegisteredResponse == false)
        {
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
            return null;
        }
        var userId = await _tokenService.GetUserIdByRefreshToken(hashedRefreshToken!);
        if (userId == -1)
        {
            return null;
        }
        var userDTO = await _userService.GetUserByIdAsync(userId);

        var existingUserToken = await _userTokenRepository.GetUserTokenByUserIdAsync(userDTO!.UserId);
        if (existingUserToken != null)
        {
            await _userTokenRepository.DeleteUserTokenAsync(existingUserToken.UserTokenId);
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

        return new RefreshTokenResponse
        {
            Token = token,
            RefreshToken = newRefreshToken
        };
    }
}
