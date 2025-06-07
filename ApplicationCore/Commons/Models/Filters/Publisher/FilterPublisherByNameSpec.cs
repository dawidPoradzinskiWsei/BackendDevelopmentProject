using ApplicationCore.Commons.Models.Parts;

public class FilterPublisherByNameSpec : Specification<GamePublisher>
{
    public FilterPublisherByNameSpec(NameParameters parameters)
    {
        if (!string.IsNullOrEmpty(parameters.Name))
        {
            Criteria = x => x.Name.Contains(parameters.Name);
        }

        Skip = (parameters.PageNumber - 1) * parameters.PageSize;
        Take = parameters.PageSize;

        if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
        {
            var asc = string.Equals(parameters.OrderDirection, "asc", StringComparison.OrdinalIgnoreCase);

            switch (parameters.OrderBy.ToLower())
            {
                case "id":
                    if (asc) OrderBy = vg => vg.Id;
                    else OrderByDescending = vg => vg.Id;
                    break;
                case "name":
                    if (asc) OrderBy = vg => vg.Name;
                    else OrderByDescending = vg => vg.Name;
                    break;
                default:
                    break;
            }
        }
    }
}