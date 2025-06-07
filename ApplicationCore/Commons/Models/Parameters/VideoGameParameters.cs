public class VideoGameParameters : PaginationParameters
{
    public string? Title { get; set; }
    public string? Console { get; set; }
    public string? Genre { get; set; }
    public string? Developer { get; set; }
    public string? Publisher { get; set; }
    public string? OrderBy { get; set; }
    public string? OrderDirection  { get; set; } = "desc";
}