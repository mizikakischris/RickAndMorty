namespace RickAndMorty.Domain.Models;

public class CharactersDomainResponse
{
    public PagingInfo PagingInfoDto { get; set; }
    public IEnumerable<Character> CharactersList { get; set; }
}
