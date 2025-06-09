using ApplicationCore.Commons.Interfaces.Repositories;
using ApplicationCore.Commons.Interfaces.Services;
using ApplicationCore.Commons.Interfaces.Services.Developer;
using ApplicationCore.Commons.Interfaces.Services.VideoGame.Admin;
using ApplicationCore.Commons.Interfaces.Services.VideoGame.User;
using ApplicationCore.Commons.Models;
using ApplicationCore.Commons.Models.Parts;
using ApplicationCore.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// kontrolery

builder.Services.AddControllers();

// baza

builder.Services.ConfigureIdentity(builder.Configuration);

// jwt

var jwtSettings = new JwtSettings();
builder.Configuration.GetSection(JwtSettings.Section).Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.ConfigureJWT(jwtSettings);

// repozyutoria

builder.Services.AddScoped<IGenericRepository<VideoGame, int>, EFGenericRepository<VideoGame, int>>();
builder.Services.AddScoped<IGenericRepository<GameImage, int>, EFGenericRepository<GameImage, int>>();
builder.Services.AddScoped<IGenericRepository<GameTitle, int>, EFGenericRepository<GameTitle, int>>();
builder.Services.AddScoped<IGenericRepository<GameConsole, int>, EFGenericRepository<GameConsole, int>>();
builder.Services.AddScoped<IGenericRepository<GameGenre, int>, EFGenericRepository<GameGenre, int>>();
builder.Services.AddScoped<IGenericRepository<GamePublisher, int>, EFGenericRepository<GamePublisher, int>>();
builder.Services.AddScoped<IGenericRepository<GameDeveloper, int>, EFGenericRepository<GameDeveloper, int>>();

// seriwsy

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IVideoGameCsvImportService, VideoGameCsvImportService>();
// builder.Services.AddScoped<IAdminVideoGameService, AdminVideoGameService>();
// builder.Services.AddScoped<IUserVideoGameService, UserVideoGameService>();
builder.Services.AddScoped<IDeveloperPublisherService<GameDeveloper>, DeveloperService>();
builder.Services.AddScoped<IDeveloperPublisherService<GamePublisher>, PublisherService>();

// Mapper

builder.Services.AddAutoMapper(typeof(VideoGameDtoMapper).Assembly);

// cors

builder.Services.ConfigureCors();

// swagger

// Swagger
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
    opt.EnableAnnotations();
});

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

app.Run();