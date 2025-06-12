using System.ComponentModel.DataAnnotations;

public class AddUserScoreDto
{
    [Required]
    [Range(0,10)]
    public float Score { get; set; }
}