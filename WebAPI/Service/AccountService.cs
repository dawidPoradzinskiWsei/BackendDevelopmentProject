
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AccountService;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<UserRole> _roleManager;

        public AccountService(UserManager<UserEntity> userManager, RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<(bool Success, string[] Errors)> RegisterUserAsync(RegisterUserDto registerDto)
        {
            var userExists = await _userManager.FindByEmailAsync(registerDto.Email);
            if (userExists != null)
                return (false, new[] { "User with this email already exists." });

            var user = new UserEntity
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            if (!await _roleManager.RoleExistsAsync("USER"))
                await _roleManager.CreateAsync(new UserRole { Name = "USER" });

            await _userManager.AddToRoleAsync(user, "USER");

            return (true, new string[0]);
        }
    }
    
}