using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApDbContext : IdentityDbContext<UserEntity>
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    
        var adminId = "4ca89fc3-d94b-41f0-97eb-e407bfc55820";
        var adminCreatedAt = new DateTime(2025,04,08);

        var adminUser = new UserEntity()
        {
            Id = adminId,
            Email = "admin@wsei.edu.pl",
            NormalizedEmail = "ADMIN@WSEI.EDU.PL",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            ConcurrencyStamp = adminId,
            SecurityStamp = adminId,
            PasswordHash = "AQAAAAIAAYagAAAAEOorPbCgHFF00KUmXPJACgxMj6TvTLFIxSLKZcrBe7TSL3L5mnw13P7IAbQsRocVQA=="
            
        };

        // PasswordHasher<UserEntity> hasher = new PasswordHasher<UserEntity>();

        // var hash = hasher.HashPassword(adminUser, "1234!");
        // Console.WriteLine(hash);

        builder.Entity<UserEntity>().HasData(adminUser);

        builder.Entity<UserEntity>().OwnsOne(v => v.Details).HasData(
            new {
                UserEntityId = adminId,
            }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=D:\\test\\database.sqlite");
    }

}