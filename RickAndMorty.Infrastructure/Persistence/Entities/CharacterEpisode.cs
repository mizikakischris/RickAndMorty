namespace RickAndMorty.Infrastructure.Persistence.Entities;

public class CharacterEpisode
{
    public int CharacterId { get; set; }
    public Character Character { get; set; } = default!;

    public string EpisodeUrl { get; set; } = default!;
}
