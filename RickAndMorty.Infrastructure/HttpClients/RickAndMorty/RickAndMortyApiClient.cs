using AutoMapper;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Domain.Models;
using RickAndMorty.Infrastructure.Models;
using RickAndMorty.Infrastructure.Services;

namespace RickAndMorty.Infrastructure.HttpClients.RickAndMorty;

public class RickAndMortyApiClient : IRickAndMortyApiClient
{
    private readonly HttpRequestService _httpService;
    private readonly IMapper _mapper;

    public RickAndMortyApiClient(HttpRequestService httpService, IMapper mapper)
    {
        _httpService = httpService;
        _mapper = mapper;
    }

    public Task<CharacterDto> GetCharacterByIdAsync(int id, CancellationToken cancellationToken)
        => _httpService.GetAsync<CharacterDto>($"character/{id}", cancellationToken);

    public async Task<CharactersDomainResponse> GetCharactersAsync(CancellationToken cancellationToken)
    {
        var charactersResponseDto = await _httpService.GetAsync<CharactersResponseDto>("character", cancellationToken);
        return _mapper.Map<CharactersDomainResponse>(charactersResponseDto);
    }
}