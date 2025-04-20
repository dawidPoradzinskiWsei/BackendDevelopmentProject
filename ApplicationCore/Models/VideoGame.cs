using ApplicationCore.Commons.Repository;

namespace ApplicationCore.Models;

public class VideoGame : IIdentity<int>
{
    public int Id { get; set; } 
    public string Title { get; set; } 
    public string Platform { get; set; } 
    public GameGenre Genre { get; set; } 
    public GamePublisher Publisher { get; set; } 
    public GameDeveloper Developer { get; set; } 
    public decimal CriticScore { get; set; } 
    public GameSales Sales { get; set; } 
    public DateTime ReleaseDate { get; set; } 
    public DateTime? LastUpdate { get; set; } 
    public string CoverImageUrl { get; set; } 
}

