namespace ApplicationCore.Commons.Interfaces.Specifications;

public interface IPagedList<T> : IList<T>
{
    public int CurrentPage { get; }
    public int TotalPages { get; }
    public int PageSize { get;}
    public int TotalCount { get; }
    public bool HasPrevious { get; }
    public bool HasNext { get; }
}