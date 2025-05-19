using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class VideoGameConfiguration : IEntityTypeConfiguration<VideoGame>
{
    public void Configure(EntityTypeBuilder<VideoGame> builder)
    {
        // Główna tabela VideoGame

        builder.ToTable("VideoGames");
        
        builder.HasKey(v => v.Id);
        builder.HasOne(v => v.Title).WithMany().HasForeignKey("TitleId");
        builder.HasOne(v => v.Platform).WithMany().HasForeignKey("PlatformId");
        builder.HasOne(v => v.Genre).WithMany().HasForeignKey("GenreId");
        builder.HasOne(v => v.Publisher).WithMany().HasForeignKey("PublisherId");
        builder.HasOne(v => v.Developer).WithMany().HasForeignKey("DeveloperId");

        builder.OwnsOne(v => v.Sales, sales =>
        {
            sales.Property(s => s.TotalSales).HasColumnName("TotalSales");
            sales.Property(s => s.NorthAmericaSales).HasColumnName("NorthAmericaSales");
            sales.Property(s => s.JapanSales).HasColumnName("JapanSales");
            sales.Property(s => s.EuropeSales).HasColumnName("EuropeSales");
            sales.Property(s => s.OtherSales).HasColumnName("OtherSales");
        });

        builder.Property(v => v.CriticScore).HasColumnType("float");
        builder.Property(v => v.ReleaseDate).IsRequired();
        builder.Property(v => v.LastUpdate).IsRequired(false);
    }
}