using ApplicationCore.Commons.Interfaces.Repositories;
using ApplicationCore.Commons.Models;
using ApplicationCore.Dto.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class UserVideoGameService : IUserVideoGameService
{
    private readonly IGenericRepository<VideoGame, int> _repo;
    private readonly IMapper _mapper;
    public UserVideoGameService(IGenericRepository<VideoGame, int> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<VideoGameResponseDto?> GetVideoGameDtoByIdAsync(int id)
    {
        var spec = new FilterVideoGameByIdSpec(id);

        var baseQuery = _repo.FindBySpecification(spec).Select(x => _mapper.Map<VideoGameResponseDto>(x)).FirstOrDefault();

        return baseQuery;
    }

    public async Task<PagedList<VideoGameResponseDto>> GetFilteredAsync(VideoGameParameters parameters)
    {
        var spec = new FilterVideoGameFullSpec(parameters);

        var baseQuery = _repo.FindBySpecification(spec).Select(x => _mapper.Map<VideoGameResponseDto>(x));

        return PagedList<VideoGameResponseDto>.ToPagedList(baseQuery, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<VideoGame?> GetByIdAsync(int id)
    {
        return await _repo.FindByIdAsync(id);
    }

}