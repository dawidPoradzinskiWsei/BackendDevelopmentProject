using ApplicationCore.Dto.Request.Auth;

namespace ApplicationCore.Commons.Interfaces.Services;

public interface IAuthService
{
    Task<string?> RegisterAsync(RegisterDto dto);
    Task<string?> LoginAsync(LoginDTO dto);
}