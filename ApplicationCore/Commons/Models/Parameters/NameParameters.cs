public class NameParameters : PaginationParameters
{
    public string? Name { get; set; }
    public string? OrderBy { get; set; }
    public string? OrderDirection { get; set; } = "desc";
}