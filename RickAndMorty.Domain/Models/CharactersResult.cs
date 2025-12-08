namespace RickAndMorty.Domain.Models;

public class CharactersResult
{
    public IReadOnlyCollection<Character> Characters { get; set; } = default!;
    public bool FromDatabase { get; set; }
}

