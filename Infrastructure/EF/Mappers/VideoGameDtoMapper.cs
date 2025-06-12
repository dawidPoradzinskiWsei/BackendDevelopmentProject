using ApplicationCore.Commons.Models;
using ApplicationCore.Dto.Request.VideoGame;
using ApplicationCore.Dto.Response;
using AutoMapper;

public class VideoGameDtoMapper : Profile
{
    public VideoGameDtoMapper()
    {
        CreateMap<VideoGame, VideoGameResponseDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title == null ? "" : src.Title.Name))
            .ForMember(dest => dest.Console, opt => opt.MapFrom(src => src.Console == null ? "" : src.Console.Name))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre == null ? "" : src.Genre.Name))
            .ForMember(dest => dest.Developer, opt => opt.MapFrom(src => src.Developer == null ? "" : src.Developer.Name))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher == null ? "" : src.Publisher.Name))
            .ForMember(dest => dest.ImageLink, opt => opt.MapFrom(src => src.Image == null ? "" : src.Image.Link))
            .ForMember(dest => dest.Sales, opt => opt.MapFrom(src => src.Sales))
            .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.CriticScore))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
            .ForMember(dest => dest.LastUpdateDate, opt => opt.MapFrom(src => src.LastUpdate))
            .ForMember(dest => dest.AverageUserScore, opt => opt.MapFrom(src => src.UserScores.Count > 0 
                ? src.UserScores.Average(us => us.Score) 
                : 0));
    }
}