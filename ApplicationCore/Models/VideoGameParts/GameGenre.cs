using ApplicationCore.Commons.Repository;

namespace ApplicationCore.Models;

public class GameGenre : IIdentity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
