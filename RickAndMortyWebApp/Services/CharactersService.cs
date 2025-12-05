using RickAndMortyWebApp.Models.Dto;

namespace RickAndMortyWebApp.Services;

public class CharactersService : BaseService, ICharactersService
{
    private readonly string _rickAndMortyUrl;

    public CharactersService(IHttpClientFactory clientFactory, IConfiguration configuration)
        : base(clientFactory)
    {
        _rickAndMortyUrl = configuration.GetValue<string>("ServiceUrls:RickAndMortyAPI");
    }

    public Task<CharactersDomainResponseDto> GetAllAsync()
    {
        return SendAsync<CharactersDomainResponseDto>(
            new APIRequest
            {
                ApiType = ApiType.GET,
                Url = $"{_rickAndMortyUrl}/api/Characters"
            }
        );
    }
}

