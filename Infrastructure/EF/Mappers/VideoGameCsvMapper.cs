using System.Globalization;
using ApplicationCore.Dto.Request.VideoGame;
using CsvHelper.Configuration;

public class VideoGameCsvMapper : ClassMap<VideoGameRequestDto>
{
    public VideoGameCsvMapper()
    {
        Map(m => m.ImageLink).Name("img");
        Map(m => m.Title).Name("title");
        Map(m => m.Console).Name("console");
        Map(m => m.Genre).Name("genre");
        Map(m => m.Publisher).Name("publisher");
        Map(m => m.Developer).Name("developer");
        Map(m => m.CriticScore).Name("critic_score").TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
        Map(m => m.TotalSales).Name("total_sales").TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
        Map(m => m.NaSales).Name("na_sales").TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
        Map(m => m.JpSales).Name("jp_sales").TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
        Map(m => m.PalSales).Name("pal_sales").TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
        Map(m => m.OtherSales).Name("other_sales").TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
        Map(m => m.ReleaseDate).Name("release_date");
        Map(m => m.LastUpdate).Name("last_update").Optional();
    }
}