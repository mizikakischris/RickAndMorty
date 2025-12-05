using Newtonsoft.Json;
using RickAndMorty.Domain.Models;

namespace RickAndMorty.Infrastructure.Models;

public class CharactersResponseDto
{
    [JsonProperty("info")]
    public PagingInfoDto PagingInfoDto { get; set; }
    [JsonProperty("results")]
    public IEnumerable<CharacterDto> CharactersList { get; set; }

}
