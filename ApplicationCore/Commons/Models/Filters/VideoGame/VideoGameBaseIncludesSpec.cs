using ApplicationCore.Commons.Models;

public abstract class VideoGameBaseIncludesSpec : Specification<VideoGame>
{
    protected VideoGameBaseIncludesSpec()
    {
        Includes.Add(vg => vg.Title);
        Includes.Add(vg => vg.Console);
        Includes.Add(vg => vg.Genre);
        Includes.Add(vg => vg.Developer);
        Includes.Add(vg => vg.Publisher);
        Includes.Add(vg => vg.Image);
        Includes.Add(vg => vg.UserScores);
    }
}