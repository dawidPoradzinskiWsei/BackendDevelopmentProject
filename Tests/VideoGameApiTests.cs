using System.Net.Http.Json;
using ApplicationCore.Commons.Models;
using ApplicationCore.Commons.Models.Parts;
using ApplicationCore.Dto.Request;
using ApplicationCore.Dto.Request.Auth;
using ApplicationCore.Dto.Request.VideoGame;
using ApplicationCore.Dto.Response;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace IntegrationTests;

public class VideoGameApiTests : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly IServiceScope _scope;
    private readonly VideoGameDbContext _dbContext;

    public VideoGameApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.Remove(
                services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<VideoGameDbContext>))
            );

                services.AddDbContext<VideoGameDbContext>(options =>
                {
                    options.UseInMemoryDatabase("aaaaa");
                });

                // _scope = services.BuildServiceProvider().CreateScope();
                // var db = scope.ServiceProvider.GetRequiredService<VideoGameDbContext>();
            });
        }).CreateClient();

        var provider = factory.Services.CreateScope().ServiceProvider;
        _scope = provider.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<VideoGameDbContext>();

    }


    public async Task InitializeAsync()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();

        var response = await _client.PostAsJsonAsync("/v1/auth/register", new RegisterDto
        {
            Email = "abc@abc.com",
            Password = "1qaz@WSX",
            Role = UserEnumRole.ADMIN,
            UserName = "abc"
        });

        string token = "";
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            data.Should().NotBeNull();
            data.TryGetValue("token", out token).Should().BeTrue();
        }
        else
        {
            response = await _client.PostAsJsonAsync("/v1/auth/login", new LoginDTO { Email = "abc@abc.com", Password = "1qaz@WSX" });

            var data = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            data.Should().NotBeNull();
            data.TryGetValue("token", out token).Should().BeTrue();
        }

        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    public Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }

    VideoGame videoGame = new VideoGame
    {
        Console = new GameConsole { Name = "console0" },
        CriticScore = 5f,
        Developer = new GameDeveloper { Name = "developer0" },
        Genre = new GameGenre { Name = "genre0" },
        Image = new GameImage { Link = "img/1" },
        LastUpdate = DateOnly.Parse("2020-02-02"),
        Publisher = new GamePublisher { Name = "publisher0" },
        Title = new GameTitle { Name = "title0" },
        Sales = new GameSales
        {
            JpSales = 0.4f,
            NaSales = 0.2f,
            OtherSales = 0.4f,
            PalSales = 0.1f,
            TotalSales = 1.1f
        },
        ReleaseDate = DateOnly.Parse("2012-04-12")
    };

    [Fact]
    public async Task registerUser_ShouldReturnOK()
    {
        var request = new RegisterDto
        {
            Email = $"{Guid.NewGuid()}@gmail.com",
            Password = "1qaz@WSX",
            Role = UserEnumRole.ADMIN,
            UserName = $"bbbb"
        };

        var response = await _client.PostAsJsonAsync("/v1/auth/register", request);
        // response.EnsureSuccessStatusCode();


        var data = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        data.Should().NotBeNull();
        data.TryGetValue("token", out var key).Should().BeTrue();

    }

    [Fact]
    public async Task registerUserAgain_ShouldReturnConflict()
    {
        var request = new RegisterDto
        {
            Email = "user@test.com",
            Password = "1qaz@WSX",
            Role = UserEnumRole.ADMIN,
            UserName = "user0"
        };

        var response = await _client.PostAsJsonAsync("/v1/auth/register", request);
        response.EnsureSuccessStatusCode();

        request.UserName = "user1";

        response = await _client.PostAsJsonAsync("/v1/auth/register", request);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task TestGenericNameTable()
    {
        // Create
        var request = new NameDto
        {
            Name = "tEsT0"
        };

        var response = await _client.PostAsJsonAsync("/v1/gameconsole", request);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Accepted);

        // Get by id

        var data = await response.Content.ReadFromJsonAsync<NameEntity>();
        var id = data!.Id;

        response = await _client.GetAsync($"v1/gameconsole/{id}");

        data = await response.Content.ReadFromJsonAsync<NameEntity>();

        data!.Name.Should().Be("tEsT0");

        // Find by name ignore case

        response = await _client.GetAsync("/v1/gameconsole?name=test0");

        response.EnsureSuccessStatusCode();

        var list = await response.Content.ReadFromJsonAsync<List<NameEntity>>();

        list![0].Id.Should().Be(id);

        //update name

        response = await _client.PutAsJsonAsync($"/v1/gameconsole/{id}", new NameDto { Name = "newName" });

        response.EnsureSuccessStatusCode();

        // check updated

        response = await _client.GetAsync($"v1/gameconsole/{id}");

        response.EnsureSuccessStatusCode();

        data = await response.Content.ReadFromJsonAsync<NameEntity>();

        data!.Name.Should().Be("newName");

        //Check that old name doesnt exist

        response = await _client.GetAsync("v1/gameconsole?name=test0");

        list = await response.Content.ReadFromJsonAsync<List<NameEntity>>();

        list!.Count.Should().Be(0);

        // Dont allow to make duplicate

        response = await _client.PostAsJsonAsync("/v1/gameconsole", new NameDto { Name = "nEwNaMe" });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Conflict);

        // Delete

        response = await _client.DeleteAsync($"/v1/gameconsole/{id}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        response = await _client.GetAsync("v1/gameconsole");

        response.EnsureSuccessStatusCode();

        list = await response.Content.ReadFromJsonAsync<List<NameEntity>>();

        list!.Count.Should().Be(0);

    }

    [Fact]
    public async Task TestVideoGameAPI()
    {
        var videoGameDto = new VideoGameRequestDto
        {
            Console = videoGame.Console!.Name,
            CriticScore = videoGame.CriticScore,
            Developer = videoGame.Developer!.Name,
            Genre = videoGame.Genre!.Name,
            ImageLink = videoGame.Image!.Link,
            Title = videoGame.Title!.Name,
            LastUpdate = videoGame.LastUpdate,
            JpSales = videoGame.Sales!.JpSales,
            NaSales = videoGame.Sales.NaSales,
            PalSales = videoGame.Sales.PalSales,
            OtherSales = videoGame.Sales.OtherSales,
            Publisher = videoGame.Publisher!.Name,
            ReleaseDate = videoGame.ReleaseDate,
            TotalSales = videoGame.Sales.TotalSales
        };

        // Create VideoGame

        var response = await _client.PostAsJsonAsync("/v1/videogame", videoGameDto);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Accepted);

        var data = await response.Content.ReadFromJsonAsync<VideoGameResponseDto>();

        data.Should().NotBeNull();

        data.Developer.Should().Be(videoGameDto.Developer);
        data.Console.Should().Be(videoGameDto.Console);
        data.Publisher.Should().Be(videoGameDto.Publisher);
        data.Title.Should().Be(videoGameDto.Title);
        data.AverageUserScore.Should().Be(0);

        var videoId = data.Id;

        // Check if Generic was also created

        response = await _client.GetAsync($"/v1/gamedeveloper?name={videoGame.Developer.Name}");

        var list = await response.Content.ReadFromJsonAsync<List<NameEntity>>();

        list!.Count.Should().BeGreaterThan(0);

        list[0].Name.Should().Be(videoGame.Developer.Name);

        var developerId = list[0].Id;

        // Change developer name

        videoGame.Developer.Name = "developer1AfterChange";

        response = await _client.PutAsJsonAsync($"/v1/gamedeveloper/{developerId}", new NameDto { Name = videoGame.Developer.Name });

        response.EnsureSuccessStatusCode();

        // Find by developer and check if id of videoGame is the same

        response = await _client.GetAsync($"/v1/videogame?developer={videoGame.Developer.Name}");

        var videoGamesList = await response.Content.ReadFromJsonAsync<List<VideoGameResponseDto>>();

        videoGamesList!.Count.Should().BeGreaterThan(0);

        videoGamesList[0].Id.Should().Be(videoId);

        // Find by id

        response = await _client.GetAsync($"/v1/videogame/{videoId}");

        response.EnsureSuccessStatusCode();

        data = await response.Content.ReadFromJsonAsync<VideoGameResponseDto>();

        data.Should().NotBeNull();

        // Change developer of videoGame and check if generic still exist

        var oldDeveloper = videoGame.Developer.Name;
        videoGame.Developer.Name = "Completly new developer";

        response = await _client.PutAsJsonAsync($"v1/videogame/{videoId}", new VideoGameRequestDto { Developer = videoGame.Developer.Name });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        response = await _client.GetAsync($"v1/videogame?developer={oldDeveloper}");

        videoGamesList = await response.Content.ReadFromJsonAsync<List<VideoGameResponseDto>>();

        videoGamesList!.Count.Should().Be(0);

        response = await _client.GetAsync($"v1/gamedeveloper?name={oldDeveloper}");

        list = await response.Content.ReadFromJsonAsync<List<NameEntity>>();

        list!.Count.Should().BeGreaterThan(0);

        // Add user score

        response = await _client.PostAsJsonAsync($"v1/videogame/{videoId}/score", new AddUserScoreDto { Score = 10 });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Accepted);

        response = await _client.GetAsync($"v1/videogame/{videoId}");

        response.EnsureSuccessStatusCode();

        data = await response.Content.ReadFromJsonAsync<VideoGameResponseDto>();

        data.Should().NotBeNull();

        data.AverageUserScore.Should().Be(10);

        // Delete user score

        response = await _client.DeleteAsync($"v1/videogame/{videoId}/score");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        response = await _client.GetAsync($"v1/videogame/{videoId}");

        response.EnsureSuccessStatusCode();

        data = await response.Content.ReadFromJsonAsync<VideoGameResponseDto>();

        data!.AverageUserScore.Should().Be(0);

        // Delete videoGame

        response = await _client.DeleteAsync($"v1/videogame/{videoId}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        response = await _client.GetAsync("v1/videogame");

        videoGamesList = await response.Content.ReadFromJsonAsync<List<VideoGameResponseDto>>();

        videoGamesList!.Count.Should().Be(0);
    }
}
