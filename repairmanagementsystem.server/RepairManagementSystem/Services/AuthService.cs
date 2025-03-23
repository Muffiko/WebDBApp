using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public AuthService(ITokenService tokenService, IUserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
    }

    public async Task<AuthResponse> AuthenticateAsync(AuthRequest request)
    {
        var userDTO = await _userService.GetUserAsync(request.Email, request.Password);
        if (userDTO == null)
        {
            return null;
        }

        var token = _tokenService.GenerateToken(userDTO);
        return new AuthResponse
        {
            Token = token,
            Email = userDTO.Email,
            Role = userDTO.Role,
            FirstName = userDTO.FirstName
        };
    }
}
