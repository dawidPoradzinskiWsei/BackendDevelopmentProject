namespace ApplicationCore.Dto.Request.VideoGame;

public class VideoGameRequestDto
{
    public string? ImageLink { get; set; }
    public string? Title { get; set; }
    public string? Console { get; set; }
    public string? Genre { get; set; }
    public string? Publisher { get; set; }
    public string? Developer { get; set; }
    public decimal? CriticScore { get; set; }
    public decimal? TotalSales { get; set; }
    public decimal? NaSales { get; set; }
    public decimal? JpSales { get; set; }
    public decimal? PalSales { get; set; }
    public decimal? OtherSales { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public DateOnly? LastUpdate { get; set; }
}