using ApplicationCore.Commons.Repository;

namespace ApplicationCore.Models;

public class GameStudio : IIdentity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
