using ApplicationCore.Commons.Models.Parts;

public class FilterPublisherByIdSpec : Specification<GamePublisher>
{
    public FilterPublisherByIdSpec(int id)
    {
        Criteria = x => x.Id == id;
    }
}