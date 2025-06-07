public class FilterVideoGameByIdSpec : VideoGameBaseIncludesSpec
{
    public FilterVideoGameByIdSpec(int id)
    {
        Criteria = x => x.Id == id;
    }
}