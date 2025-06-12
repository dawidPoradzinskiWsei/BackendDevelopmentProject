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
    public float? CriticScore { get; set; }
    public float? TotalSales { get; set; }
    public float? NaSales { get; set; }
    public float? JpSales { get; set; }
    public float? PalSales { get; set; }
    public float? OtherSales { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public DateOnly? LastUpdate { get; set; }
}