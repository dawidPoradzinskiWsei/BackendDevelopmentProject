using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_VideoGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameDeveloper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDeveloper", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameGenre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamePublisher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePublisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameTitle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTitle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TitleId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlatformId = table.Column<int>(type: "INTEGER", nullable: false),
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    PublisherId = table.Column<int>(type: "INTEGER", nullable: false),
                    DeveloperId = table.Column<int>(type: "INTEGER", nullable: false),
                    CriticScore = table.Column<decimal>(type: "float", nullable: false),
                    TotalSales = table.Column<decimal>(type: "TEXT", nullable: false),
                    NorthAmericaSales = table.Column<decimal>(type: "TEXT", nullable: false),
                    JapanSales = table.Column<decimal>(type: "TEXT", nullable: false),
                    EuropeSales = table.Column<decimal>(type: "TEXT", nullable: false),
                    OtherSales = table.Column<decimal>(type: "TEXT", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoGames_GameDeveloper_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "GameDeveloper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoGames_GameGenre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "GameGenre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoGames_GamePlatform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "GamePlatform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoGames_GamePublisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "GamePublisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoGames_GameTitle_TitleId",
                        column: x => x.TitleId,
                        principalTable: "GameTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_DeveloperId",
                table: "VideoGames",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_GenreId",
                table: "VideoGames",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_PlatformId",
                table: "VideoGames",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_PublisherId",
                table: "VideoGames",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_TitleId",
                table: "VideoGames",
                column: "TitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoGames");

            migrationBuilder.DropTable(
                name: "GameDeveloper");

            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropTable(
                name: "GamePlatform");

            migrationBuilder.DropTable(
                name: "GamePublisher");

            migrationBuilder.DropTable(
                name: "GameTitle");
        }
    }
}
