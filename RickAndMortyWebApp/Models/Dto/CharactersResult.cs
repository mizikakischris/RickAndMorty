using RickAndMorty.Domain.Models;

namespace RickAndMortyWebApp.Models.Dto;

public class CharactersResult
{
    public CharactersDomainResponse Characters { get; set; } = default!;
    public bool FromDatabase { get; set; }
}
