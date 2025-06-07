using ApplicationCore.Commons.Models.Parts;

public class FilterPublisherByNameSpec : Specification<GamePublisher>
{
    public FilterPublisherByNameSpec(string name)
    {
        Criteria = x => x.Name == name;
    }
}