using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApplicationCore.Commons.Interfaces.Services;
using ApplicationCore.Configuration;
using ApplicationCore.Dto.Request.Auth;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Identity;

public class AuthService : IAuthService
{

    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<UserRole> _roleManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<UserEntity> userManager, RoleManager<UserRole> roleManager, JwtSettings jwtSettings)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtSettings;
    }

    public async Task<string?> LoginAsync(LoginDTO dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            throw new UnauthorizedAccessException();
        }

        return await CreateToken(user);
    }

    public async Task<string?> RegisterAsync(RegisterDto dto)
    {
        var user = new UserEntity
        {
            Email = dto.Email,
            UserName = dto.UserName
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        if (!await _roleManager.RoleExistsAsync(dto.Role.ToString()))
        {
            await _roleManager.CreateAsync(new UserRole { Name = dto.Role.ToString() });
        }

        await _userManager.AddToRoleAsync(user, dto.Role.ToString());
        return await CreateToken(user);
    }
    

    private async Task<string> CreateToken(UserEntity user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            .AddClaim("userId", user.Id)
            .AddClaim(JwtRegisteredClaimNames.Name, user.UserName)
            .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
            .AddClaim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds())
            .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid())
            .AddClaim(System.Security.Claims.ClaimTypes.Role, roles)
            .Audience(_jwtSettings.Audience)
            .Issuer(_jwtSettings.Issuer)
            .Encode();
    }
}