namespace RickAndMorty.Infrastructure.Persistence.Entities;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string Species { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string Image { get; set; } = default!;
    public string Url { get; set; } = default!;
    public DateTime Created { get; set; }
    public int OriginId { get; set; }
    public CharacterLocation Origin { get; set; } = default!;
    public int LocationId { get; set; }
    public CharacterLocation Location { get; set; } = default!;

    public ICollection<CharacterEpisode> Episodes { get; set; } = new List<CharacterEpisode>();
}
