using System.Security.Principal;
using ApplicationCore.Commons.Repository;

public class GameTitle : IIdentity<int>
{
    public int Id { get; set; }
    public string Title { get; set; }
}