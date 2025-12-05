using RickAndMorty.Domain.Models;
using RickAndMortyWebApp.Models.Dto;
using CharactersResult = RickAndMortyWebApp.Models.Dto.CharactersResult;

namespace RickAndMortyWebApp.Services;

public class CharactersService : BaseService, ICharactersService
{
    private readonly string _rickAndMortyUrl;

    public CharactersService(IHttpClientFactory clientFactory, IConfiguration configuration)
        : base(clientFactory)
    {
        _rickAndMortyUrl = configuration.GetValue<string>("ServiceUrls:RickAndMortyAPI");
    }

    public Task<CharactersResult> GetAllAsync()
    {
        return SendAsync<CharactersResult>(
            new APIRequest
            {
                ApiType = ApiType.GET,
                Url = $"{_rickAndMortyUrl}/api/Characters"
            }
        );
    }
}

