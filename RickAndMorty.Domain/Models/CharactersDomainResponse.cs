namespace RickAndMorty.Domain.Models;

public class CharactersDomainResponse
{
    public PagingInfo PagingInfo { get; set; }
    public IEnumerable<Character> CharactersList { get; set; }
}
