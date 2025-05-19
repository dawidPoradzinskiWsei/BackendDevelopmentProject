using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

public static class Configure
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                "CorsPolicy",
                builder =>
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<UserDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("Users")))
            .AddIdentity<UserEntity, UserRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
            })
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(this IServiceCollection services, JwtSettings jwtSettings)
    {

        services
            .AddAuthorization(opt =>
            {
                opt.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build()
                );
                opt.AddPolicy("Email", policy =>
                {
                    policy.RequireClaim("email");
                });

            })
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;

                if (jwtSettings.Secret != null)
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ClockSkew = TimeSpan.FromSeconds(60)
                    };
                }

                opt.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenException))
                        {
                            context.Response.Headers.Add("Token-expired", "true");
                        }
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject("401 Not authorized");
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject("403 Not authorized");
                        return context.Response.WriteAsync(result);
                    }
                };
            });
    }

    public static async Task AddUsers(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
            var find = await userManager.FindByEmailAsync("test@wsei.edu.pl");
            if (find == null)
            {
                UserEntity user = new UserEntity() { Email = "test@wsei.edu.pl", UserName = "test123" };

                var saved = await userManager?.CreateAsync(user, "1234ABcd$");

                if (saved.Succeeded)
                {
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();
                    if (!await roleManager.RoleExistsAsync("USER"))
                    {
                        await roleManager.CreateAsync(new UserRole { Name = "USER" });
                    }

                    await userManager.AddToRoleAsync(user, "USER");
                }
            }
        }
    }
}