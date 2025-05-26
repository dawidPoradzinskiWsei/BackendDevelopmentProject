namespace ApplicationCore.Interfaces.AccountService;


public interface IAccountService
{
    Task<(bool Success, string[] Errors)> RegisterUserAsync(RegisterUserDto registerDto);
    
}