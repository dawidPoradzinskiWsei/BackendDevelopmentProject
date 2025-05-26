using ApplicationCore.Interfaces.AccountService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Service dla kontrolerów

builder.Services.AddControllers();

// JWT

builder.Services.ConfigureIdentity(builder.Configuration);

builder.Services.AddSingleton<JwtSettings>();
builder.Services.ConfigureJWT(new JwtSettings(builder.Configuration));
builder.Services.ConfigureCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
            Enter 'Bearer' and then your token in the text input below.
            Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Video Game API",
    });

});
builder.Services.AddScoped<IAccountService, AccountService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<VideoGameDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("VideoGame")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.AddUsers();

app.Run();
