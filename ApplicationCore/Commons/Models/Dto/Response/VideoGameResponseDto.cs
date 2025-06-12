using ApplicationCore.Commons.Models.Parts;

namespace ApplicationCore.Dto.Response;

public class VideoGameResponseDto
{
    public int Id { get; set; }
    public string? ImageLink { get; set; }
    public string? Title { get; set; }
    public string? Console { get; set; }
    public string? Genre { get; set; }
    public string? Developer { get; set; }
    public string? Publisher { get; set; }
    public GameSales? Sales { get; set; }
    public float? Score { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public DateOnly? LastUpdateDate { get; set; }
    public float AverageUserScore { get; set; }
    
}