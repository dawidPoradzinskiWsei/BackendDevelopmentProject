using ApplicationCore.Commons.Models.Parts;

public class FilterDeveloperByIdSpec : Specification<GameDeveloper>
{
    public FilterDeveloperByIdSpec(int id)
    {
        Criteria = x => x.Id == id;
    }
}