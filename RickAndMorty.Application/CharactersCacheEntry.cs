using RickAndMorty.Domain.Models;

namespace RickAndMorty.Application;

public class CharactersCacheEntry
{
    public CharactersDomainResponse Data { get; set; } = default!;
    public DateTime CachedAt { get; set; }
}

