using ApplicationCore.Commons.Models.Parts;

public class FilterDeveloperByNameSpec : Specification<GameDeveloper>
{
    public FilterDeveloperByNameSpec(int id)
    {
        Criteria = x => x.Id == id;
    }
}