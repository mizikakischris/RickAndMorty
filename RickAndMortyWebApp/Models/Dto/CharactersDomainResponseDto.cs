namespace RickAndMortyWebApp.Models.Dto;

public class CharactersDomainResponseDto
{
    public PagingInfo PagingInfo { get; set; }
    public IEnumerable<CharacterDto> CharactersList { get; set; }
}
