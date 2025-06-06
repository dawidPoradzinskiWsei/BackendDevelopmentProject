namespace ApplicationCore.Commons.Interfaces.Specifications;

public interface IPaginationParameters
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}