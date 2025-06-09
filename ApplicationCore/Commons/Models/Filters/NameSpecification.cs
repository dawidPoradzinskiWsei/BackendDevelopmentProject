using System.Linq.Expressions;
using System.Security.Principal;
using ApplicationCore.Commons.Interfaces;

public class NameSpecification<T> : Specification<T> where T : NameEntity, new()
{
    public NameSpecification(NameParameters parameters)
    {

        if (!string.IsNullOrEmpty(parameters.Name))
        {
            var nameProperty = typeof(T).GetProperty("Name");
            Criteria = x => x.Name.ToLower().Contains(parameters.Name.ToLower());
        }

        // Paginacja
        Skip = (parameters.PageNumber - 1) * parameters.PageSize;
        Take = parameters.PageSize;

        // Sortowanie
        if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
        {
            ApplySorting(parameters.OrderBy, parameters.OrderDirection);
        }
    }
    private void ApplySorting(string orderBy, string orderDirection)
    {
        var asc = string.Equals(orderDirection, "asc", StringComparison.OrdinalIgnoreCase);

        switch (orderBy.ToLower())
        {
            case "id":
                OrderBy = asc ? x => x.Id : null;
                OrderByDescending = !asc ? x => x.Id : null;
                break;
            case "name":
                OrderBy = asc ? x => x.Name : null;
                OrderByDescending = !asc ? x => x.Name : null;
                break;
            default:
                break;
        }
    }
}