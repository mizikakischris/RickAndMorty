using RickAndMorty.Domain.Models;

namespace RickAndMorty.Application;

public class CharactersCacheInfo
{
    public DateTime LastFetchUtc { get; set; }
    public List<Character> Characters { get; set; } = [];
}
