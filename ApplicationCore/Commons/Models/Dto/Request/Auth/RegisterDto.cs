using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApplicationCore.Commons.Models;

namespace ApplicationCore.Dto.Request.Auth;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [Length(8,20)]
    public string Password { get; set; } = string.Empty;
    [EnumDataType(typeof(UserEnumRole))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserEnumRole Role { get; set; }
}