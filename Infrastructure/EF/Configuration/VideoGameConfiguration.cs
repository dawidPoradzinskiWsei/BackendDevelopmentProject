using ApplicationCore.Commons.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class VideoGameConfiguration : IEntityTypeConfiguration<VideoGame>
{
    public void Configure(EntityTypeBuilder<VideoGame> builder)
    {
        builder.ToTable("VideoGames");

        builder.HasKey(v => v.Id);

        builder.HasOne(v => v.Title).WithMany().HasForeignKey("TitleId").OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(v => v.Console).WithMany().HasForeignKey("ConsoleId").OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(v => v.Genre).WithMany().HasForeignKey("GenreId").OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(v => v.Publisher).WithMany().HasForeignKey("PublisherId").OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(v => v.Developer).WithMany().HasForeignKey("Developerid").OnDelete(DeleteBehavior.SetNull);

        builder.OwnsOne(v => v.Sales, sales =>
        {
            sales.Property(s => s.TotalSales).HasColumnName("TotalSales");
            sales.Property(s => s.NaSales).HasColumnName("NaSales");
            sales.Property(s => s.JpSales).HasColumnName("JpSales");
            sales.Property(s => s.PalSales).HasColumnName("PalSales");
            sales.Property(s => s.OtherSales).HasColumnName("OtherSales");
        });

        builder.Property(v => v.CriticScore).HasColumnType("float");
        builder.Property(v => v.ReleaseDate).IsRequired(false);
        builder.Property(v => v.LastUpdate).IsRequired(false);

        builder.HasMany(v => v.UserScores)
            .WithOne(r => r.VideoGame)
            .HasForeignKey(r => r.VideoGameId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}