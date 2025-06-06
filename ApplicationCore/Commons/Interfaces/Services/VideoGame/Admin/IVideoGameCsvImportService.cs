namespace ApplicationCore.Commons.Interfaces.Services.VideoGame.Admin;

public interface IVideoGameCsvImportService
{
    Task<int> UploadCsvAsync(Stream csvStream);
}