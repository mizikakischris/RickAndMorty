using RickAndMorty.Domain.Models;

namespace RickAndMorty.WebApi.Responses;

public class CharactersListResponse
{
    public IEnumerable<CharactersResponse> Results { get; set; }
}

public class CharactersResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Species { get; set; }
    public string Type { get; set; }
    public string Gender { get; set; }
    public CharacterLocation Origin { get; set; }
    public CharacterLocation Location { get; set; }
    public string Image { get; set; }
    public List<string> Episode { get; set; }
    public string Url { get; set; }
    public DateTime Created { get; set; }
}
