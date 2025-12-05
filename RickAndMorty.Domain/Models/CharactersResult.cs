namespace RickAndMorty.Domain.Models;

public class CharactersResult
{
    public CharactersDomainResponse Characters { get; set; } = default!;
    public bool FromDatabase { get; set; }
}

