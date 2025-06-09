using ApplicationCore.Commons.Models;

public class FilterVideoGameFullSpec : VideoGameBaseIncludesSpec
{

    public FilterVideoGameFullSpec(VideoGameParameters parameters)
    {
        if (!string.IsNullOrEmpty(parameters.Title))
        {
            AndAlso(x => x.Title.Name.ToUpper().Contains(parameters.Title.ToUpper()));
        }

        if (!string.IsNullOrEmpty(parameters.Console))
        {
            AndAlso(x => x.Console.Name.ToUpper().Contains(parameters.Console.ToUpper()));
        }

        if (!string.IsNullOrEmpty(parameters.Genre))
        {
            AndAlso(x => x.Genre.Name.ToUpper().Contains(parameters.Genre.ToUpper()));
        }

        if (!string.IsNullOrEmpty(parameters.Developer))
        {
            AndAlso(x => x.Developer.Name.ToUpper().Contains(parameters.Developer.ToUpper()));
        }

        if (!string.IsNullOrEmpty(parameters.Publisher))
        {
            AndAlso(x => x.Publisher.Name.ToUpper().Contains(parameters.Publisher.ToUpper()));
        }

        SetOrdering(parameters.OrderBy, parameters.OrderDirection);

        Skip = (parameters.PageNumber - 1) * parameters.PageSize;
        Take = parameters.PageSize;
    }
    

    private void SetOrdering(string? orderBy, string? direction)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return;

        var asc = string.Equals(direction, "asc", StringComparison.OrdinalIgnoreCase);

        switch (orderBy.ToLower())
        {
            case "id":
                if (asc) OrderBy = vg => vg.Id;
                else OrderByDescending = vg => vg.Id;
                break;
            case "title":
                if (asc) OrderBy = vg => vg.Title.Name;
                else OrderByDescending = vg => vg.Title.Name;
                break;

            case "releasedate":
                if (asc) OrderBy = vg => vg.ReleaseDate;
                else OrderByDescending = vg => vg.ReleaseDate;
                break;
            case "updatedate":
                if (asc) OrderBy = vg => vg.LastUpdate;
                else OrderByDescending = vg => vg.LastUpdate;
                break;

            case "score":
                if (asc) OrderBy = vg => vg.CriticScore;
                else OrderByDescending = vg => vg.CriticScore;
                break;

            default:
                break;
        }
    }
}