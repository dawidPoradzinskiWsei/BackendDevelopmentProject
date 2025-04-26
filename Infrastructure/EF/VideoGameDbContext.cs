using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;

public class VideoGameDbContext : DbContext {

    public VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : base(options) {}

    public DbSet<VideoGame> VideoGames {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VideoGameConfiguration());
    }

}