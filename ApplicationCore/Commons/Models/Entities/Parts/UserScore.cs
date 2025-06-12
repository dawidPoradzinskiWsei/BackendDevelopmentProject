using System.Text.Json.Serialization;
using ApplicationCore.Commons.Models;

public class UserScore
{
    public int VideoGameId { get; set; }
    public int UserId { get; set; }
    public float Score { get; set; }
    [JsonIgnore]
    public VideoGame VideoGame { get; set; }
}