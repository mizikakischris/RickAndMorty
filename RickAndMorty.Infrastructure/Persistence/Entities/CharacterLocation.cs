namespace RickAndMorty.Infrastructure.Persistence.Entities;

public class CharacterLocation
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
}
