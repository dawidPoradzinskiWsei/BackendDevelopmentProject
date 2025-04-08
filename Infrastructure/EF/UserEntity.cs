using Microsoft.AspNetCore.Identity;

public class UserEntity : IdentityUser
{
    public UserDetails Details {get;set;}
}