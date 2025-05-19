using System.Security.Principal;
using ApplicationCore.Commons.Repository;

public class GamePlatform : IIdentity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    
}