using ApplicationCore.Commons.Interfaces;

namespace ApplicationCore.Commons.Models.Parts;

public class GameImage : IIdentity<int>
{
    public int Id { get; set; }
    public string Link { get; set; }
}