using ApplicationCore.Commons.Interfaces;
using ApplicationCore.Commons.Models.Parts;

namespace ApplicationCore.Commons.Models;

public class VideoGame : IIdentity<int>
{
    public int Id { get; set; }
    public GameImage? Image { get; set; }
    public GameTitle? Title { get; set; }
    public GameConsole? Console { get; set; }
    public GameGenre? Genre { get; set; }
    public GamePublisher? Publisher { get; set; }
    public GameDeveloper? Developer { get; set; }
    public float? CriticScore { get; set; }
    public GameSales? Sales { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public DateOnly? LastUpdate { get; set; }
    
    public ICollection<UserScore> UserScores { get; set; } = new List<UserScore>();

}