using System.Security.Principal;
using ApplicationCore.Commons.Interfaces;

public class NameEntity : IIdentity<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
}