using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Dto.Request.VideoGame;

public class VideoGameRequestDto
{
    [Required]
    public string? ImageLink { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Console { get; set; }
    [Required]
    public string? Genre { get; set; }
    [Required]
    public string? Publisher { get; set; }
    [Required]
    public string? Developer { get; set; }
    [Required]
    [Range(0, 10)]
    public decimal? CriticScore { get; set; }
    public decimal? TotalSales { get; set; }
    public decimal? NaSales { get; set; }
    public decimal? JpSales { get; set; }
    public decimal? PalSales { get; set; }
    public decimal? OtherSales { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public DateOnly? LastUpdate { get; set; }
}