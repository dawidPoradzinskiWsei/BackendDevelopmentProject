using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Dto.Request.Auth;

public class LoginDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [Length(8,20)]
    public string Password { get; set; } = string.Empty;
}