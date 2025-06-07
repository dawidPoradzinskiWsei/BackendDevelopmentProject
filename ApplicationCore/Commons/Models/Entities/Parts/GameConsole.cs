using ApplicationCore.Commons.Interfaces;

namespace ApplicationCore.Commons.Models.Parts;

public class GameConsole : IIdentity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
}