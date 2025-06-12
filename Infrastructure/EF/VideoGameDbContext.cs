using ApplicationCore.Commons.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class VideoGameDbContext : IdentityDbContext<UserEntity, UserRole, int>
{
    public VideoGameDbContext(DbContextOptions options) : base(options) { }

    public DbSet<VideoGame> VideoGames { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new VideoGameConfiguration());

        builder.Entity<UserScore>().HasKey(us => new { us.VideoGameId, us.UserId });
    }
}